using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs.Extensions;

public static class AgentAssignmentsExtensions
{
    public static AgentAssignment ToDto(this AgentAssignmentDbModel model)
    {
        return new AgentAssignment
        {
            Agent = model.Agent,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Property = model.PropertyId,
            Score = model.Score,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AgentAssignmentDbModel ToModel(
        this AgentAssignmentUpdateInput updateDto,
        AgentAssignmentWhereUniqueInput uniqueId
    )
    {
        var agentAssignment = new AgentAssignmentDbModel
        {
            Id = uniqueId.Id,
            Agent = updateDto.Agent,
            Score = updateDto.Score
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            agentAssignment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            agentAssignment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return agentAssignment;
    }
}
