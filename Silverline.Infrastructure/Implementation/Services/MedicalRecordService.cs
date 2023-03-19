using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Implementation.Services;

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
    }
}
