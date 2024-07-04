namespace RealEstateCrm.APIs.Dtos;

public class ClientCreateInput
{
    public List<Appointment>? Appointments { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime UpdatedAt { get; set; }
}
