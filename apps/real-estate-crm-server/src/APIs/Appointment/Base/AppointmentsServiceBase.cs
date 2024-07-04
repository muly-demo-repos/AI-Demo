using Microsoft.EntityFrameworkCore;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;
using RealEstateCrm.APIs.Extensions;
using RealEstateCrm.Infrastructure;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs;

public abstract class AppointmentsServiceBase : IAppointmentsService
{
    protected readonly RealEstateCrmDbContext _context;

    public AppointmentsServiceBase(RealEstateCrmDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a Client record for Appointment
    /// </summary>
    public async Task<Client> GetClient(AppointmentWhereUniqueInput uniqueId)
    {
        var appointment = await _context
            .Appointments.Where(appointment => appointment.Id == uniqueId.Id)
            .Include(appointment => appointment.Client)
            .FirstOrDefaultAsync();
        if (appointment == null)
        {
            throw new NotFoundException();
        }
        return appointment.Client.ToDto();
    }

    /// <summary>
    /// Meta data about Appointment records
    /// </summary>
    public async Task<MetadataDto> AppointmentsMeta(AppointmentFindManyArgs findManyArgs)
    {
        var count = await _context.Appointments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Create one Appointment
    /// </summary>
    public async Task<Appointment> CreateAppointment(AppointmentCreateInput createDto)
    {
        var appointment = new AppointmentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            DateTime = createDto.DateTime,
            Location = createDto.Location,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            appointment.Id = createDto.Id;
        }
        if (createDto.Client != null)
        {
            appointment.Client = await _context
                .Clients.Where(client => createDto.Client.Id == client.Id)
                .FirstOrDefaultAsync();
        }

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AppointmentDbModel>(appointment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Appointment
    /// </summary>
    public async Task DeleteAppointment(AppointmentWhereUniqueInput uniqueId)
    {
        var appointment = await _context.Appointments.FindAsync(uniqueId.Id);
        if (appointment == null)
        {
            throw new NotFoundException();
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Appointments
    /// </summary>
    public async Task<List<Appointment>> Appointments(AppointmentFindManyArgs findManyArgs)
    {
        var appointments = await _context
            .Appointments.Include(x => x.Client)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return appointments.ConvertAll(appointment => appointment.ToDto());
    }

    /// <summary>
    /// Get one Appointment
    /// </summary>
    public async Task<Appointment> Appointment(AppointmentWhereUniqueInput uniqueId)
    {
        var appointments = await this.Appointments(
            new AppointmentFindManyArgs { Where = new AppointmentWhereInput { Id = uniqueId.Id } }
        );
        var appointment = appointments.FirstOrDefault();
        if (appointment == null)
        {
            throw new NotFoundException();
        }

        return appointment;
    }

    /// <summary>
    /// Update one Appointment
    /// </summary>
    public async Task UpdateAppointment(
        AppointmentWhereUniqueInput uniqueId,
        AppointmentUpdateInput updateDto
    )
    {
        var appointment = updateDto.ToModel(uniqueId);

        _context.Entry(appointment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Appointments.Any(e => e.Id == appointment.Id))
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
