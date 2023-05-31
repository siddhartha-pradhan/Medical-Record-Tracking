using Silverline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Core.ViewModels
{
	public class AppointmentDetailsViewModel
	{
		public Guid AppointmentId { get; set; }

		public string DoctorImage { get; set; }

		public string DoctorName { get; set; }

		public string Specialty { get; set; }	

		public string HighestMedicalDegree { get; set; }

		public string BookedDate { get; set; }	

		public string AppointedDate { get; set; }

		public string FinalizedDate { get; set; }

		public string Request { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public List<MedicationTreatmentViewModel> MedicationTreatments { get; set; }

		public List<LaboratoryDiagnosisViewModel> LaboratoryDiagnosis { get; set; }
	}
}
