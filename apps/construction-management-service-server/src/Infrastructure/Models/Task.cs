using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConstructionManagementService.Core.Enums;

namespace ConstructionManagementService.Infrastructure.Models;

[Table("Tasks")]
public class TaskDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))]
    public ProjectDbModel? Project { get; set; } = null;

    public StatusEnum? Status { get; set; }

    [StringLength(1000)]
    public string? TaskName { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
