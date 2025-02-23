using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Core.ViewModels
{
	public class AdminViewModel
	{
		public int PatientCount { get; set; }

		public int DoctorCount { get; set; }

		public int DiagnosticCount { get; set; }

		public int MedicationCount { get; set; }	
	}
}
