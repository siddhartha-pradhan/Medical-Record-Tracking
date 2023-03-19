using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Implementation.Services;

public class AppointmentDetailService : IAppointmentDetailService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentDetailService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void FinalizeAppointment(AppointmentDetail appointment)
    {
        _unitOfWork.AppointmentDetail.Add(appointment);
        appointment.Appointment.AppointmentStatus = Constants.Completed;
        _unitOfWork.Save();
    }

    public List<AppointmentDetail> GetAllAppointments()
    {
        return _unitOfWork.AppointmentDetail.GetAll();
    }

    public AppointmentDetail GetAppointmentDetail(Guid Id)
    {
        return _unitOfWork.AppointmentDetail.GetFirstOrDefault(x => x.Id == Id);
    }
}
