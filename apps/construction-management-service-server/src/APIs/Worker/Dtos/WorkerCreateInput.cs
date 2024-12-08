namespace ConstructionManagementService.APIs.Dtos;

public class WorkerCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Role { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? WorkerName { get; set; }
}
