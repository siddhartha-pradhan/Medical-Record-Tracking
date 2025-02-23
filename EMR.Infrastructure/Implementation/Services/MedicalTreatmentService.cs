﻿using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Constants;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Implementation.Services;

public class MedicalTreatmentService : IMedicalTreatmentService
{
	private readonly IUnitOfWork _unitOfWork;

	public MedicalTreatmentService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public void CompleteCourse(Guid treatmentId)
	{
		var treatment = _unitOfWork.MedicalTreatment.Get(treatmentId);
		treatment.Status = Constants.Completed;
		treatment.IsCompleted = true;
		_unitOfWork.Save();
	}

	public List<MedicationTreatment> GetMedicationTreatments()
	{
		var result = _unitOfWork.MedicalTreatment.GetAll();
		
		return result;
	}

	public void UpdatePrescriptions(MedicationTreatment treatment)
	{
		var result = _unitOfWork.MedicalTreatment.GetAll().Where(x => x.Id == treatment.Id).FirstOrDefault();

		if (result != null)
		{
			result.PharmacistRemarks = treatment.PharmacistRemarks;
			result.PharmacistId = treatment.PharmacistId;
			result.FinalizedDate = DateTime.Now;
			result.ActionStatus = Constants.Completed;
		}

		_unitOfWork.Save();
	}
}
