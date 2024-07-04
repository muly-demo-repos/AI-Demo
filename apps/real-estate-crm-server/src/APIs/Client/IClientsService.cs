using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;

namespace RealEstateCrm.APIs;

public interface IClientsService
{
    /// <summary>
    /// Connect multiple Appointments records to Client
    /// </summary>
    public Task ConnectAppointments(
        ClientWhereUniqueInput uniqueId,
        AppointmentWhereUniqueInput[] appointmentsId
    );

    /// <summary>
    /// Disconnect multiple Appointments records from Client
    /// </summary>
    public Task DisconnectAppointments(
        ClientWhereUniqueInput uniqueId,
        AppointmentWhereUniqueInput[] appointmentsId
    );

    /// <summary>
    /// Find multiple Appointments records for Client
    /// </summary>
    public Task<List<Appointment>> FindAppointments(
        ClientWhereUniqueInput uniqueId,
        AppointmentFindManyArgs AppointmentFindManyArgs
    );

    /// <summary>
    /// Meta data about Client records
    /// </summary>
    public Task<MetadataDto> ClientsMeta(ClientFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple Appointments records for Client
    /// </summary>
    public Task UpdateAppointments(
        ClientWhereUniqueInput uniqueId,
        AppointmentWhereUniqueInput[] appointmentsId
    );

    /// <summary>
    /// Create one Client
    /// </summary>
    public Task<Client> CreateClient(ClientCreateInput client);

    /// <summary>
    /// Delete one Client
    /// </summary>
    public Task DeleteClient(ClientWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Clients
    /// </summary>
    public Task<List<Client>> Clients(ClientFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Client
    /// </summary>
    public Task<Client> Client(ClientWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Client
    /// </summary>
    public Task UpdateClient(ClientWhereUniqueInput uniqueId, ClientUpdateInput updateDto);
}
