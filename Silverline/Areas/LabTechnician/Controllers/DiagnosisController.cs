using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Silverline.Areas.LabTechnician.Controllers;

[Area("LabTechnician")]
[Authorize(Roles = Constants.LabTechnician)]
public class DiagnosisController : Controller
{
	public IActionResult Diagnosis()
	{
		return View();
	}

	public IActionResult History()
	{
		return View();
	}

	public IActionResult Requested()
	{
		return View();
	}
}
