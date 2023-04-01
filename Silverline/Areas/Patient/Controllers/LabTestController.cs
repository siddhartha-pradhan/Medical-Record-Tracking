using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
[Authorize(Roles = Constants.Patient)]
public class LabTestController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

}
