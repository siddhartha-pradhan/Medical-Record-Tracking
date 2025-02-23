using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IAppointmentDetailService
{
	List<AppointmentDetail> GetAllAppointments();

	AppointmentDetail GetAppointmentDetail(Guid Id);

	void FinalizeAppointment(AppointmentDetail appointment);
}
