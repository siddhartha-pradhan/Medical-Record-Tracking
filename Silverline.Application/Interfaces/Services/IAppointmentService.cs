using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IAppointmentService
{
    List<Appointment> GetAllAppointments(Guid Id);

    List<Appointment> GetAllBookedAppointment(Guid Id);

    Appointment GetAppointment(Guid Id);

    void BookAppointment(Appointment appointment);

}
