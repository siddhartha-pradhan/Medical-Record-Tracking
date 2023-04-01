using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Silverline.Areas.Pharmacist.Controllers;

[Area("Pharmacist")]
[Authorize(Roles = Constants.Pharmacist)]
public class HomeController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
