using RealEstateCrm.Core.Enums;

namespace RealEstateCrm.APIs.Dtos;

public class PropertyCreateInput
{
    public string? Address { get; set; }

    public List<AgentAssignment>? AgentAssignments { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public double? Price { get; set; }

    public int? Size { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
