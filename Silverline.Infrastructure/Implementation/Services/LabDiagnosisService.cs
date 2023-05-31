using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Implementation.Services;

public class LabDiagnosisService : ILabDiagnosisService
{
    private readonly IUnitOfWork _unitOfWork;

    public LabDiagnosisService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<LaboratoryDiagnosis> GetAllLabDiagnosis()
    {
        var result = _unitOfWork.LabDiagnosis.GetAll();

        return result;
    }

    public void AddLabDiagnosis(LaboratoryDiagnosis diagnosis)
    {
        _unitOfWork.LabDiagnosis.Add(diagnosis);
        _unitOfWork.Save();
    }

    public void UpdateLabDiagnosis(LaboratoryDiagnosis diagnosis)
    {
        var result = _unitOfWork.LabDiagnosis.GetAll().Where(x => x.Id == diagnosis.Id).FirstOrDefault();

        if (result != null)
        {
            result.Value = diagnosis.Value;
            result.TechnicianRemarks = diagnosis.TechnicianRemarks;
            result.TechnicianId = diagnosis.TechnicianId;
            result.FinalizedDate = DateTime.Now;
            result.ActionStatus = Constants.Completed;
            result.Status = Constants.Completed;
        }

        _unitOfWork.Save();
    }
}
