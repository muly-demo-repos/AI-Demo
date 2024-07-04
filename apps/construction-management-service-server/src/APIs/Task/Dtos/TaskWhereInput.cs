using ConstructionManagementService.Core.Enums;

namespace ConstructionManagementService.APIs.Dtos;

public class TaskWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Id { get; set; }

    public string? Project { get; set; }

    public StatusEnum? Status { get; set; }

    public string? TaskName { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
