namespace RealEstateCrm.APIs.Dtos;

public class Appointment
{
    public string? Client { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DateTime { get; set; }

    public string Id { get; set; }

    public string? Location { get; set; }

    public DateTime UpdatedAt { get; set; }
}
