﻿using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Silverline.Areas.Pharmacist.Controllers;

[Area("Pharmacist")]
[Authorize(Roles = Constants.Pharmacist)]
public class PrescriptionController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
