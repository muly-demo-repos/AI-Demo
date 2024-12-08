using Microsoft.EntityFrameworkCore;
using RealEstateCrm.APIs;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.APIs.Errors;
using RealEstateCrm.APIs.Extensions;
using RealEstateCrm.Infrastructure;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs;

public abstract class AgentAssignmentsServiceBase : IAgentAssignmentsService
{
    protected readonly RealEstateCrmDbContext _context;

    public AgentAssignmentsServiceBase(RealEstateCrmDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a Property record for AgentAssignment
    /// </summary>
    public async Task<Property> GetProperty(AgentAssignmentWhereUniqueInput uniqueId)
    {
        var agentAssignment = await _context
            .AgentAssignments.Where(agentAssignment => agentAssignment.Id == uniqueId.Id)
            .Include(agentAssignment => agentAssignment.Property)
            .FirstOrDefaultAsync();
        if (agentAssignment == null)
        {
            throw new NotFoundException();
        }
        return agentAssignment.Property.ToDto();
    }

    /// <summary>
    /// Meta data about AgentAssignment records
    /// </summary>
    public async Task<MetadataDto> AgentAssignmentsMeta(AgentAssignmentFindManyArgs findManyArgs)
    {
        var count = await _context.AgentAssignments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Create one AgentAssignment
    /// </summary>
    public async Task<AgentAssignment> CreateAgentAssignment(AgentAssignmentCreateInput createDto)
    {
        var agentAssignment = new AgentAssignmentDbModel
        {
            Agent = createDto.Agent,
            CreatedAt = createDto.CreatedAt,
            Score = createDto.Score,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            agentAssignment.Id = createDto.Id;
        }
        if (createDto.Property != null)
        {
            agentAssignment.Property = await _context
                .Properties.Where(property => createDto.Property.Id == property.Id)
                .FirstOrDefaultAsync();
        }

        _context.AgentAssignments.Add(agentAssignment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AgentAssignmentDbModel>(agentAssignment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one AgentAssignment
    /// </summary>
    public async Task DeleteAgentAssignment(AgentAssignmentWhereUniqueInput uniqueId)
    {
        var agentAssignment = await _context.AgentAssignments.FindAsync(uniqueId.Id);
        if (agentAssignment == null)
        {
            throw new NotFoundException();
        }

        _context.AgentAssignments.Remove(agentAssignment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many AgentAssignments
    /// </summary>
    public async Task<List<AgentAssignment>> AgentAssignments(
        AgentAssignmentFindManyArgs findManyArgs
    )
    {
        var agentAssignments = await _context
            .AgentAssignments.Include(x => x.Property)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return agentAssignments.ConvertAll(agentAssignment => agentAssignment.ToDto());
    }

    /// <summary>
    /// Get one AgentAssignment
    /// </summary>
    public async Task<AgentAssignment> AgentAssignment(AgentAssignmentWhereUniqueInput uniqueId)
    {
        var agentAssignments = await this.AgentAssignments(
            new AgentAssignmentFindManyArgs
            {
                Where = new AgentAssignmentWhereInput { Id = uniqueId.Id }
            }
        );
        var agentAssignment = agentAssignments.FirstOrDefault();
        if (agentAssignment == null)
        {
            throw new NotFoundException();
        }

        return agentAssignment;
    }

    /// <summary>
    /// Update one AgentAssignment
    /// </summary>
    public async Task UpdateAgentAssignment(
        AgentAssignmentWhereUniqueInput uniqueId,
        AgentAssignmentUpdateInput updateDto
    )
    {
        var agentAssignment = updateDto.ToModel(uniqueId);

        _context.Entry(agentAssignment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.AgentAssignments.Any(e => e.Id == agentAssignment.Id))
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
