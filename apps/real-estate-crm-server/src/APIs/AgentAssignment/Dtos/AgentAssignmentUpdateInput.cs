namespace RealEstateCrm.APIs.Dtos;

public class AgentAssignmentUpdateInput
{
    public string? Agent { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Property { get; set; }

    public int? Score { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
