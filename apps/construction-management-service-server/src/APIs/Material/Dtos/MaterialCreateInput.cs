namespace ConstructionManagementService.APIs.Dtos;

public class MaterialCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? MaterialName { get; set; }

    public int? Quantity { get; set; }

    public string? Unit { get; set; }

    public DateTime UpdatedAt { get; set; }
}
