using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RealEstateCrm.Core.Enums;

namespace RealEstateCrm.Infrastructure.Models;

[Table("Properties")]
public class PropertyDbModel
{
    [StringLength(1000)]
    public string? Address { get; set; }

    public List<AgentAssignmentDbModel>? AgentAssignments { get; set; } =
        new List<AgentAssignmentDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    [Range(-999999999, 999999999)]
    public int? Size { get; set; }

    public StatusEnum? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
