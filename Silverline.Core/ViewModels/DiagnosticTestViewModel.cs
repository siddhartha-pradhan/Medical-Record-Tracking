using Microsoft.AspNetCore.Mvc.Rendering;
using Silverline.Core.Entities;

namespace Silverline.Core.ViewModels;

public class DiagnosticTestViewModel
{
    public DiagnosticTest DiagnosticTest { get; set; }

    public IEnumerable<SelectListItem>? TestTypes { get; set; }

}
