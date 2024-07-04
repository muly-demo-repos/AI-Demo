using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateCrm.Infrastructure.Models;

[Table("AgentAssignments")]
public class AgentAssignmentDbModel
{
    [StringLength(1000)]
    public string? Agent { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? PropertyId { get; set; }

    [ForeignKey(nameof(PropertyId))]
    public PropertyDbModel? Property { get; set; } = null;

    [Range(-999999999, 999999999)]
    public int? Score { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
