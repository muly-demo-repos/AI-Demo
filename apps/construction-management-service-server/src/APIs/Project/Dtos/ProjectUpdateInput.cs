namespace ConstructionManagementService.APIs.Dtos;

public class ProjectUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Id { get; set; }

    public string? ProjectName { get; set; }

    public DateTime? StartDate { get; set; }

    public List<string>? Tasks { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
