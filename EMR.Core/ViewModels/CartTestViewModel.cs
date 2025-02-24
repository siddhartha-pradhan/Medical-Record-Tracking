using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Core.ViewModels
{
    public class CartTestViewModel
    {
        public Guid CartId { get; set; }

        public Guid TestId { get; set; }

        [Display(Name = "Test Title")]
        public string TestTitle { get; set; }

        public Guid PatientId { get; set; }

        public string PatientName { get; set; }

        public string PatientImage { get; set; }

        [Display(Name = "Test Range")]
        public string TestRange { get; set; }

        [Display(Name = "Test Value")]
        public float TestValue { get; set; }

        [Display(Name = "Technician Remarks")]
        public string TechnicianRemarks { get; set; }
    }
}
