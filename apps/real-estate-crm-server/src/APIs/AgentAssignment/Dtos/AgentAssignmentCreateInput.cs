namespace RealEstateCrm.APIs.Dtos;

public class AgentAssignmentCreateInput
{
    public string? Agent { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Property? Property { get; set; }

    public int? Score { get; set; }

    public DateTime UpdatedAt { get; set; }
}
