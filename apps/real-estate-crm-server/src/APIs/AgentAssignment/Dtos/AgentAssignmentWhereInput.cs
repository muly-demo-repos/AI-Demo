namespace RealEstateCrm.APIs.Dtos;

public class AgentAssignmentWhereInput
{
    public string? Agent { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Property { get; set; }

    public int? Score { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
