using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Entities;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class TestController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITestService _testService;

	public TestController(IUnitOfWork unitOfWork, ITestService testService)
	{
		_unitOfWork = unitOfWork;
		_testService = testService;
	}

	#region Razor Pages
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Upsert(Guid? id)
	{
		var test = new DiagnosticTest();

		if (id == null)
		{
			return View(test);
		}

		test = _unitOfWork.Test.GetFirstOrDefault(x => x.Id == id);

		if (test == null)
		{
			return NotFound();
		}

		return View(test);
	}

	public IActionResult Delete(Guid id)
	{
		var test = _unitOfWork.Test.GetFirstOrDefault(x => x.Id == id);

		if (test == null)
		{
			return NotFound();
		}

		return View(test);
	}
	#endregion

	#region API Calls
	[HttpGet]
	public IActionResult GetAll()
	{
		var test = _testService.GetAllDiagnosticTests();
		return Json(new { data = test });
	}

	[HttpPost, ActionName("Upsert")]
	public IActionResult UpsertDiagnosticTest(DiagnosticTest test)
	{
		if (ModelState.IsValid)
		{
			if (test.Id == Guid.Empty)
			{
				_testService.AddDiagnosticTest(test);
				TempData["Success"] = "Diagnostic Test Added Successfully";
			}
			else
			{
				_testService.UpdateDiagnosticTest(test);
				TempData["Info"] = "Diagnostic Test Updated Successfully";
			}

			return RedirectToAction("Index");
		}

		return View(test);

	}

	[HttpPost, ActionName("Delete")]
	public IActionResult DeleteDiagnosticTest(Guid id)
	{
		var test = _unitOfWork.Test.GetFirstOrDefault(x => x.Id == id);

		if (ModelState.IsValid)
		{
			if (test != null)
			{
				_testService.DeleteDiagnosticTest(test);
				TempData["Delete"] = "Diagnostic Test Delete Successfully";
			}
			else
			{
				return NotFound();
			}
		}

		return View(test);
	}
	#endregion
}
