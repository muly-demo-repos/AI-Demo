using Microsoft.EntityFrameworkCore;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;
using RealEstateCrm.APIs.Extensions;
using RealEstateCrm.Infrastructure;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs;

public abstract class PropertiesServiceBase : IPropertiesService
{
    protected readonly RealEstateCrmDbContext _context;

    public PropertiesServiceBase(RealEstateCrmDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Property
    /// </summary>
    public async Task<Property> CreateProperty(PropertyCreateInput createDto)
    {
        var property = new PropertyDbModel
        {
            Address = createDto.Address,
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Price = createDto.Price,
            Size = createDto.Size,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            property.Id = createDto.Id;
        }
        if (createDto.AgentAssignments != null)
        {
            property.AgentAssignments = await _context
                .AgentAssignments.Where(agentAssignment =>
                    createDto.AgentAssignments.Select(t => t.Id).Contains(agentAssignment.Id)
                )
                .ToListAsync();
        }

        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PropertyDbModel>(property.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Property
    /// </summary>
    public async Task DeleteProperty(PropertyWhereUniqueInput uniqueId)
    {
        var property = await _context.Properties.FindAsync(uniqueId.Id);
        if (property == null)
        {
            throw new NotFoundException();
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Properties
    /// </summary>
    public async Task<List<Property>> Properties(PropertyFindManyArgs findManyArgs)
    {
        var properties = await _context
            .Properties.Include(x => x.AgentAssignments)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return properties.ConvertAll(property => property.ToDto());
    }

    /// <summary>
    /// Get one Property
    /// </summary>
    public async Task<Property> Property(PropertyWhereUniqueInput uniqueId)
    {
        var properties = await this.Properties(
            new PropertyFindManyArgs { Where = new PropertyWhereInput { Id = uniqueId.Id } }
        );
        var property = properties.FirstOrDefault();
        if (property == null)
        {
            throw new NotFoundException();
        }

        return property;
    }

    /// <summary>
    /// Meta data about Property records
    /// </summary>
    public async Task<MetadataDto> PropertiesMeta(PropertyFindManyArgs findManyArgs)
    {
        var count = await _context.Properties.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Connect multiple AgentAssignments records to Property
    /// </summary>
    public async Task ConnectAgentAssignments(
        PropertyWhereUniqueInput uniqueId,
        AgentAssignmentWhereUniqueInput[] agentAssignmentsId
    )
    {
        var property = await _context
            .Properties.Include(x => x.AgentAssignments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (property == null)
        {
            throw new NotFoundException();
        }

        var agentAssignments = await _context
            .AgentAssignments.Where(t => agentAssignmentsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (agentAssignments.Count == 0)
        {
            throw new NotFoundException();
        }

        var agentAssignmentsToConnect = agentAssignments.Except(property.AgentAssignments);

        foreach (var agentAssignment in agentAssignmentsToConnect)
        {
            property.AgentAssignments.Add(agentAssignment);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple AgentAssignments records from Property
    /// </summary>
    public async Task DisconnectAgentAssignments(
        PropertyWhereUniqueInput uniqueId,
        AgentAssignmentWhereUniqueInput[] agentAssignmentsId
    )
    {
        var property = await _context
            .Properties.Include(x => x.AgentAssignments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (property == null)
        {
            throw new NotFoundException();
        }

        var agentAssignments = await _context
            .AgentAssignments.Where(t => agentAssignmentsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var agentAssignment in agentAssignments)
        {
            property.AgentAssignments?.Remove(agentAssignment);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple AgentAssignments records for Property
    /// </summary>
    public async Task<List<AgentAssignment>> FindAgentAssignments(
        PropertyWhereUniqueInput uniqueId,
        AgentAssignmentFindManyArgs propertyFindManyArgs
    )
    {
        var agentAssignments = await _context
            .AgentAssignments.Where(m => m.PropertyId == uniqueId.Id)
            .ApplyWhere(propertyFindManyArgs.Where)
            .ApplySkip(propertyFindManyArgs.Skip)
            .ApplyTake(propertyFindManyArgs.Take)
            .ApplyOrderBy(propertyFindManyArgs.SortBy)
            .ToListAsync();

        return agentAssignments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple AgentAssignments records for Property
    /// </summary>
    public async Task UpdateAgentAssignments(
        PropertyWhereUniqueInput uniqueId,
        AgentAssignmentWhereUniqueInput[] agentAssignmentsId
    )
    {
        var property = await _context
            .Properties.Include(t => t.AgentAssignments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (property == null)
        {
            throw new NotFoundException();
        }

        var agentAssignments = await _context
            .AgentAssignments.Where(a => agentAssignmentsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (agentAssignments.Count == 0)
        {
            throw new NotFoundException();
        }

        property.AgentAssignments = agentAssignments;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Property
    /// </summary>
    public async Task UpdateProperty(
        PropertyWhereUniqueInput uniqueId,
        PropertyUpdateInput updateDto
    )
    {
        var property = updateDto.ToModel(uniqueId);

        if (updateDto.AgentAssignments != null)
        {
            property.AgentAssignments = await _context
                .AgentAssignments.Where(agentAssignment =>
                    updateDto.AgentAssignments.Select(t => t).Contains(agentAssignment.Id)
                )
                .ToListAsync();
        }

        _context.Entry(property).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Properties.Any(e => e.Id == property.Id))
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
