using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionManagementService.Infrastructure.Models;

[Table("Projects")]
public class ProjectDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? ProjectName { get; set; }

    public DateTime? StartDate { get; set; }

    public List<TaskDbModel>? Tasks { get; set; } = new List<TaskDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
