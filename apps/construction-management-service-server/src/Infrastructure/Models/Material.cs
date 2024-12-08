using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionManagementService.Infrastructure.Models;

[Table("Materials")]
public class MaterialDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? MaterialName { get; set; }

    [Range(-999999999, 999999999)]
    public int? Quantity { get; set; }

    [StringLength(1000)]
    public string? Unit { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
