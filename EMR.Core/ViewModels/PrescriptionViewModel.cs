﻿namespace Silverline.Core.ViewModels;

public class PrescriptionViewModel
{
	public Guid Referral { get; set; }

	public string PatientName { get; set; }

	public string PatientImage { get; set; }

	public List<PrescriptionDiagnosisViewModel> PrescriptionDiagnosis { get; set; }
}
