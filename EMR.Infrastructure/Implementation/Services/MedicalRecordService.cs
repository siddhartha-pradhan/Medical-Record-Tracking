using EMR.Application.Interfaces.Repositories;
using EMR.Application.Interfaces.Services;
using EMR.Core.Entities;

namespace EMR.Infrastructure.Implementation.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IUnitOfWork _unitOfWork;

    public MedicalRecordService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddMedicalRecord(MedicalRecord medicalRecord)
    {
        _unitOfWork.MedicalRecord.Add(medicalRecord);

        _unitOfWork.Save();
    }

	public List<MedicalRecord> GetAllMedicalRecords(Guid patientId)
	{
		var result = _unitOfWork.MedicalRecord.GetAll().Where(x => x.PatientId == patientId).ToList();  
        
        return result;
	}
}
