using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Core.ViewModels
{
	public class DoctorPatientViewModel
	{
		public string UserId { get; set; }

		public Guid PatientId { get; set; }

		public string PatientName { get; set; }
		
		public string PhoneNumber { get; set; }

		public string Address { get; set; }

		public string Street { get; set; }

		public string DateOfBirth { get; set; }
	}
}
