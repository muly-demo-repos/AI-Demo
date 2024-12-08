using RealEstateCrm.APIs.Dtos;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs.Extensions;

public static class ClientsExtensions
{
    public static Client ToDto(this ClientDbModel model)
    {
        return new Client
        {
            Appointments = model.Appointments?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            Id = model.Id,
            Name = model.Name,
            PhoneNumber = model.PhoneNumber,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ClientDbModel ToModel(
        this ClientUpdateInput updateDto,
        ClientWhereUniqueInput uniqueId
    )
    {
        var client = new ClientDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            Name = updateDto.Name,
            PhoneNumber = updateDto.PhoneNumber
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            client.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            client.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return client;
    }
}
