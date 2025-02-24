using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface IAppointmentDetailService
{
	List<AppointmentDetail> GetAllAppointments();

	AppointmentDetail GetAppointmentDetail(Guid Id);

	void FinalizeAppointment(AppointmentDetail appointment);
}
