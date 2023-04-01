using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Silverline.Areas.LabTechnician.Controllers;

[Area("LabTechnician")]
[Authorize(Roles = Constants.LabTechnician)]
public class DiagnosisController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
