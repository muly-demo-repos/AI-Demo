using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;

namespace RealEstateCrm.APIs;

public interface IPropertiesService
{
    /// <summary>
    /// Create one Property
    /// </summary>
    public Task<Property> CreateProperty(PropertyCreateInput property);

    /// <summary>
    /// Delete one Property
    /// </summary>
    public Task DeleteProperty(PropertyWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Properties
    /// </summary>
    public Task<List<Property>> Properties(PropertyFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Property
    /// </summary>
    public Task<Property> Property(PropertyWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Property records
    /// </summary>
    public Task<MetadataDto> PropertiesMeta(PropertyFindManyArgs findManyArgs);

    /// <summary>
    /// Connect multiple AgentAssignments records to Property
    /// </summary>
    public Task ConnectAgentAssignments(
        PropertyWhereUniqueInput uniqueId,
        AgentAssignmentWhereUniqueInput[] agentAssignmentsId
    );

    /// <summary>
    /// Disconnect multiple AgentAssignments records from Property
    /// </summary>
    public Task DisconnectAgentAssignments(
        PropertyWhereUniqueInput uniqueId,
        AgentAssignmentWhereUniqueInput[] agentAssignmentsId
    );

    /// <summary>
    /// Find multiple AgentAssignments records for Property
    /// </summary>
    public Task<List<AgentAssignment>> FindAgentAssignments(
        PropertyWhereUniqueInput uniqueId,
        AgentAssignmentFindManyArgs AgentAssignmentFindManyArgs
    );

    /// <summary>
    /// Update multiple AgentAssignments records for Property
    /// </summary>
    public Task UpdateAgentAssignments(
        PropertyWhereUniqueInput uniqueId,
        AgentAssignmentWhereUniqueInput[] agentAssignmentsId
    );

    /// <summary>
    /// Update one Property
    /// </summary>
    public Task UpdateProperty(PropertyWhereUniqueInput uniqueId, PropertyUpdateInput updateDto);
}
