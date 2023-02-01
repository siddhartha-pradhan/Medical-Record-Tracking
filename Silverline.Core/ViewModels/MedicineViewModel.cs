using Microsoft.AspNetCore.Mvc.Rendering;
using Silverline.Core.Entities;

namespace Silverline.Core.ViewModels
{
    public class MedicineViewModel
    {
        public Medicine Medicine { get; set; }
        
        public IEnumerable<SelectListItem>? Manufacturerers { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }

    }
}
