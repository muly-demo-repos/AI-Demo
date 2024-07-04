using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;

namespace RealEstateCrm.APIs;

public interface IAgentAssignmentsService
{
    /// <summary>
    /// Get a Property record for AgentAssignment
    /// </summary>
    public Task<Property> GetProperty(AgentAssignmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about AgentAssignment records
    /// </summary>
    public Task<MetadataDto> AgentAssignmentsMeta(AgentAssignmentFindManyArgs findManyArgs);

    /// <summary>
    /// Create one AgentAssignment
    /// </summary>
    public Task<AgentAssignment> CreateAgentAssignment(AgentAssignmentCreateInput agentassignment);

    /// <summary>
    /// Delete one AgentAssignment
    /// </summary>
    public Task DeleteAgentAssignment(AgentAssignmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many AgentAssignments
    /// </summary>
    public Task<List<AgentAssignment>> AgentAssignments(AgentAssignmentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one AgentAssignment
    /// </summary>
    public Task<AgentAssignment> AgentAssignment(AgentAssignmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one AgentAssignment
    /// </summary>
    public Task UpdateAgentAssignment(
        AgentAssignmentWhereUniqueInput uniqueId,
        AgentAssignmentUpdateInput updateDto
    );
}
