using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Core.ViewModels
{
    public class CartTestViewModel
    {
        public Guid CartId { get; set; }

        public Guid TestId { get; set; }

        public string TestTitle { get; set; }

        public Guid PatientId { get; set; }

        public string PatientName { get; set; }

        public string PatientImage { get; set; }

        public string TestRange { get; set; }   

        public float TestValue { get; set; }

        public string TechnicianRemarks { get; set; }
    }
}
