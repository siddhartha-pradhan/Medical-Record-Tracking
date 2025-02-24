using Microsoft.EntityFrameworkCore;
using EMR.Application.Interfaces.Repositories;
using EMR.Application.Interfaces.Services;
using EMR.Core.Constants;
using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;

namespace EMR.Infrastructure.Implementation.Services;

public class AppointmentDetailService : IAppointmentDetailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _dbContext;

    public AppointmentDetailService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
    }

    public void FinalizeAppointment(AppointmentDetail appointment)
    {
        var detailId = Guid.NewGuid();

		var result = new AppointmentDetail()
        {
            Id = detailId,
            AppointmentTitle = appointment.AppointmentTitle,
            AppointmentDescription = appointment.AppointmentDescription,
            AppointmentId = appointment.AppointmentId,
        };

        _unitOfWork.AppointmentDetail.Add(result);
		
        _unitOfWork.Save();

		foreach (var item in appointment.MedicalTreatments)
        {
            if(item.MedicineId == Guid.Empty)
            {
                break;
            }

			var medicationId = Guid.NewGuid();

            var medicineId = item.MedicineId.ToString().ToUpper();

			var sql = "INSERT INTO MedicationTreatments (Id, MedicineId, Dose, IsCompleted, TimePeriod, TimeFormat, ReferralId, DoctorRemarks, Status, ActionStatus) " +
			         $"VALUES ('{medicationId}', '{medicineId}', '{item.Dose}', 0, '{item.TimePeriod}', '{item.TimeFormat}', '{detailId}', '{item.DoctorRemarks}', 'Ongoing', 'Pending')";

			_dbContext.Database.ExecuteSqlRaw(sql);

            _dbContext.SaveChanges();
        }

        foreach (var item in appointment.LaboratoryDiagnosis)
        {
			if (item.TestId == Guid.Empty)
			{
				break;
			}

			var diagnosisId = Guid.NewGuid();
			
            var sql = "INSERT INTO LaboratoryDiagnosis (Id, TestId, ReferralId, DoctorRemarks, Status, ActionStatus) " +
					 $"VALUES ('{diagnosisId}', '{item.TestId}', '{detailId}', '{item.DoctorRemarks}', 'Ongoing', 'Pending')";

			_dbContext.Database.ExecuteSqlRaw(sql);

			_dbContext.SaveChanges();
		}

		_unitOfWork.Appointment.GetFirstOrDefault(x => x.Id == appointment.AppointmentId).PaymentStatus = Constants.Completed;

		_unitOfWork.Appointment.GetFirstOrDefault(x => x.Id == appointment.AppointmentId).AppointmentStatus = Constants.Completed;

		_unitOfWork.Appointment.GetFirstOrDefault(x => x.Id == appointment.AppointmentId).FinalizedTime = DateTime.Now;

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
