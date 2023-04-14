using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IAppointmentService
{
    List<Appointment> GetAllFinalizedAppointmentsList();

    List<Appointment> GetAllBookedAppointments();

    List<Appointment> GetAllFinalizedAppointments();

    List<Appointment> GetAllAppointments(Guid Id);

    List<Appointment> GetAllBookedAppointment(Guid Id);

    Appointment GetAppointment(Guid Id);

    void BookAppointment(Appointment appointment);

}
