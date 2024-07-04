using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateCrm.Infrastructure.Models;

[Table("Appointments")]
public class AppointmentDbModel
{
    public string? ClientId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public ClientDbModel? Client { get; set; } = null;

    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? DateTime { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Location { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
