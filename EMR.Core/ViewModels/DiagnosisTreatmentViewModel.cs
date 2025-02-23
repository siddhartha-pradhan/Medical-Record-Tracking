﻿namespace Silverline.Core.ViewModels;

public class DiagnosisTreatmentViewModel
{
	public Guid TreatmentId { get; set; }

	public string MedicineName { get; set; }

	public string Dose { get; set; }

	public string TimePeriod { get; set; }

	public string TimeFormat { get; set; }

	public string Remarks { get; set; }

	public string ReferredBy { get; set; }

	public string Status { get; set; }
}
