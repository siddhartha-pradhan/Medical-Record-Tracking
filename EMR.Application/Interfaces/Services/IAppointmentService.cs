using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface IAppointmentService
{
    List<Appointment> GetAllFinalizedAppointmentsList();

	List<Appointment> GetAllListAppointments();

	List<Appointment> GetAllBookedAppointments();

    List<Appointment> GetAllFinalizedAppointments();

    List<Appointment> GetAllAppointments(Guid Id);

    List<Appointment> GetAllBookedAppointment(Guid Id);

    Appointment GetAppointment(Guid Id);

    void BookAppointment(Appointment appointment);

    void CancelAppointment(Guid Id);

	void AddEmergencyAppointment(Appointment appointment);
}
