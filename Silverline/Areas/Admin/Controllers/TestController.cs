using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Entities;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Silverline.Core.ViewModels;
using Silverline.Infrastructure.Implementation.Services;
using Microsoft.EntityFrameworkCore;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class TestController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITestService _testService;
	private readonly ITestTypeService _testTypeService;

	public TestController(IUnitOfWork unitOfWork, ITestService testService, ITestTypeService testTypeService)
	{
		_unitOfWork = unitOfWork;
		_testService = testService;
		_testTypeService = testTypeService;
	}

	#region Razor Pages
	public IActionResult Index()
	{
        var tests = _testService.GetAllDiagnosticTests().AsQueryable().Include(x => x.TestType).ToList();
        var testTypes = _testTypeService.GetAllTestTypes();

        //var result = (from test in tests
        //			 join testType in testTypes
        //			 on test.ClassId equals testType.Id
        //			 select new
        //			 {
        //				 Id = test.Id,
        //				 Title = test.Title,
        //				 InitialRange = test.InitialRange,
        //				 FinalRange = test.FinalRange,
        //				 Unit = test.Unit,
        //				 UnitPrice = test.UnitPrice,
        //				 TestType = testType.Name
        //			 }).ToList();


        return View(tests);
    }


	public IActionResult Upsert(Guid? id)
	{
		var test = new DiagnosticTest();

		var tests = _testService.GetAllDiagnosticTests().AsQueryable().Include(x => x.TestType).ToList();

        if (id == null)
		{
			ViewBag.ClassId = new SelectList(_testTypeService.GetAllTestTypes(), "Id", "Name");
			return View();
		}
		test = _unitOfWork.Test.GetFirstOrDefault(x => x.Id == id);

		if (test == null)
		{
			return NotFound();
		}
		ViewBag.ClassId = new SelectList(_testTypeService.GetAllTestTypes(), "Id", "Name", test.ClassId);

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
		var tests = _testService.GetAllDiagnosticTests().AsQueryable().Include(x=>x.TestType).ToList();
		var testTypes = _testTypeService.GetAllTestTypes();

		//var result = (from test in tests
		//			 join testType in testTypes
		//			 on test.ClassId equals testType.Id
		//			 select new
		//			 {
		//				 Id = test.Id,
		//				 Title = test.Title,
		//				 InitialRange = test.InitialRange,
		//				 FinalRange = test.FinalRange,
		//				 Unit = test.Unit,
		//				 UnitPrice = test.UnitPrice,
		//				 TestType = testType.Name
		//			 }).ToList();


        return View(tests);
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
