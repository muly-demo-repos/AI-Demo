using Microsoft.EntityFrameworkCore;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;
using RealEstateCrm.APIs.Extensions;
using RealEstateCrm.Infrastructure;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs;

public abstract class ClientsServiceBase : IClientsService
{
    protected readonly RealEstateCrmDbContext _context;

    public ClientsServiceBase(RealEstateCrmDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Connect multiple Appointments records to Client
    /// </summary>
    public async Task ConnectAppointments(
        ClientWhereUniqueInput uniqueId,
        AppointmentWhereUniqueInput[] appointmentsId
    )
    {
        var client = await _context
            .Clients.Include(x => x.Appointments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (client == null)
        {
            throw new NotFoundException();
        }

        var appointments = await _context
            .Appointments.Where(t => appointmentsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (appointments.Count == 0)
        {
            throw new NotFoundException();
        }

        var appointmentsToConnect = appointments.Except(client.Appointments);

        foreach (var appointment in appointmentsToConnect)
        {
            client.Appointments.Add(appointment);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Appointments records from Client
    /// </summary>
    public async Task DisconnectAppointments(
        ClientWhereUniqueInput uniqueId,
        AppointmentWhereUniqueInput[] appointmentsId
    )
    {
        var client = await _context
            .Clients.Include(x => x.Appointments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (client == null)
        {
            throw new NotFoundException();
        }

        var appointments = await _context
            .Appointments.Where(t => appointmentsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var appointment in appointments)
        {
            client.Appointments?.Remove(appointment);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Appointments records for Client
    /// </summary>
    public async Task<List<Appointment>> FindAppointments(
        ClientWhereUniqueInput uniqueId,
        AppointmentFindManyArgs clientFindManyArgs
    )
    {
        var appointments = await _context
            .Appointments.Where(m => m.ClientId == uniqueId.Id)
            .ApplyWhere(clientFindManyArgs.Where)
            .ApplySkip(clientFindManyArgs.Skip)
            .ApplyTake(clientFindManyArgs.Take)
            .ApplyOrderBy(clientFindManyArgs.SortBy)
            .ToListAsync();

        return appointments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Client records
    /// </summary>
    public async Task<MetadataDto> ClientsMeta(ClientFindManyArgs findManyArgs)
    {
        var count = await _context.Clients.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Appointments records for Client
    /// </summary>
    public async Task UpdateAppointments(
        ClientWhereUniqueInput uniqueId,
        AppointmentWhereUniqueInput[] appointmentsId
    )
    {
        var client = await _context
            .Clients.Include(t => t.Appointments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (client == null)
        {
            throw new NotFoundException();
        }

        var appointments = await _context
            .Appointments.Where(a => appointmentsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (appointments.Count == 0)
        {
            throw new NotFoundException();
        }

        client.Appointments = appointments;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Create one Client
    /// </summary>
    public async Task<Client> CreateClient(ClientCreateInput createDto)
    {
        var client = new ClientDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Name = createDto.Name,
            PhoneNumber = createDto.PhoneNumber,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            client.Id = createDto.Id;
        }
        if (createDto.Appointments != null)
        {
            client.Appointments = await _context
                .Appointments.Where(appointment =>
                    createDto.Appointments.Select(t => t.Id).Contains(appointment.Id)
                )
                .ToListAsync();
        }

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ClientDbModel>(client.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Client
    /// </summary>
    public async Task DeleteClient(ClientWhereUniqueInput uniqueId)
    {
        var client = await _context.Clients.FindAsync(uniqueId.Id);
        if (client == null)
        {
            throw new NotFoundException();
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Clients
    /// </summary>
    public async Task<List<Client>> Clients(ClientFindManyArgs findManyArgs)
    {
        var clients = await _context
            .Clients.Include(x => x.Appointments)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return clients.ConvertAll(client => client.ToDto());
    }

    /// <summary>
    /// Get one Client
    /// </summary>
    public async Task<Client> Client(ClientWhereUniqueInput uniqueId)
    {
        var clients = await this.Clients(
            new ClientFindManyArgs { Where = new ClientWhereInput { Id = uniqueId.Id } }
        );
        var client = clients.FirstOrDefault();
        if (client == null)
        {
            throw new NotFoundException();
        }

        return client;
    }

    /// <summary>
    /// Update one Client
    /// </summary>
    public async Task UpdateClient(ClientWhereUniqueInput uniqueId, ClientUpdateInput updateDto)
    {
        var client = updateDto.ToModel(uniqueId);

        if (updateDto.Appointments != null)
        {
            client.Appointments = await _context
                .Appointments.Where(appointment =>
                    updateDto.Appointments.Select(t => t).Contains(appointment.Id)
                )
                .ToListAsync();
        }

        _context.Entry(client).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Clients.Any(e => e.Id == client.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
