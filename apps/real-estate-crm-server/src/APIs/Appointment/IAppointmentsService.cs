using RealEstateCrm.APIs.Common;
using RealEstateCrm.APIs.Dtos;

namespace RealEstateCrm.APIs;

public interface IAppointmentsService
{
    /// <summary>
    /// Get a Client record for Appointment
    /// </summary>
    public Task<Client> GetClient(AppointmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Appointment records
    /// </summary>
    public Task<MetadataDto> AppointmentsMeta(AppointmentFindManyArgs findManyArgs);

    /// <summary>
    /// Create one Appointment
    /// </summary>
    public Task<Appointment> CreateAppointment(AppointmentCreateInput appointment);

    /// <summary>
    /// Delete one Appointment
    /// </summary>
    public Task DeleteAppointment(AppointmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Appointments
    /// </summary>
    public Task<List<Appointment>> Appointments(AppointmentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Appointment
    /// </summary>
    public Task<Appointment> Appointment(AppointmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Appointment
    /// </summary>
    public Task UpdateAppointment(
        AppointmentWhereUniqueInput uniqueId,
        AppointmentUpdateInput updateDto
    );
}
