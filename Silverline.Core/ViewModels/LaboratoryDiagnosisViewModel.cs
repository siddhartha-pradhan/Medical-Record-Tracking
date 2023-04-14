using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Core.ViewModels
{
	public class LaboratoryDiagnosisViewModel
	{
		public Guid Id { get; set; }

		public Guid TestId { get; set; }

		public Guid ReferralId { get; set; }

		public string TestName { get; set; }	

		public string DoctorRemarks { get; set; }
	}
}
