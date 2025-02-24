using Microsoft.AspNetCore.Mvc.Rendering;
using EMR.Core.Entities;

namespace EMR.Core.ViewModels;

public class DiagnosticTestViewModel
{
    public DiagnosticTest DiagnosticTest { get; set; }

    public IEnumerable<SelectListItem>? TestTypes { get; set; }

}
