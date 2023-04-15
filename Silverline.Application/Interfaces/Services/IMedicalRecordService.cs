using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IMedicalRecordService
{
    void AddMedicalRecord(MedicalRecord medicalRecord);

    List<MedicalRecord> GetAllMedicalRecords(Guid patientId);
}
