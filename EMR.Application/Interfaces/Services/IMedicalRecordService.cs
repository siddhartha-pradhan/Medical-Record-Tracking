using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface IMedicalRecordService
{
    void AddMedicalRecord(MedicalRecord medicalRecord);

    List<MedicalRecord> GetAllMedicalRecords(Guid patientId);
}
