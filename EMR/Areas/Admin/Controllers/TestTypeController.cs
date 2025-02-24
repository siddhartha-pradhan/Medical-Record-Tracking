using Microsoft.AspNetCore.Mvc;
using EMR.Core.Entities;
using EMR.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using EMR.Application.Interfaces.Services;
using EMR.Application.Interfaces.Repositories;

namespace EMR.Areas.Admin.Controllers;

[Area("Admin")]
	[Authorize(Roles = Constants.Admin)]
	public class TestTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITestTypeService _testTypeService;

    public TestTypeController(IUnitOfWork unitOfWork, 
        ITestTypeService testTypeService)
    {
        _unitOfWork = unitOfWork;
        _testTypeService = testTypeService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(Guid? id)
    {
        var testType = new TestType();

        if (id == null)
        {
            return View(testType);
        }

        testType = _unitOfWork.TestType.GetFirstOrDefault(x => x.Id == id);

        if (testType == null)
        {
            return NotFound();
        }

        return View(testType);
    }

    public IActionResult Delete(Guid id)
    {
        var testType = _unitOfWork.TestType.GetFirstOrDefault(x => x.Id == id);

        if (testType == null)
        {
            return NotFound();
        }

        return View(testType);
    }
    #endregion

    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        var testTypes = _testTypeService.GetAllTestTypes();

        return Json(new { data = testTypes });
    }

    [HttpPost, ActionName("Upsert")]
    public IActionResult UpsertTestType(TestType testType)
    {
        if (ModelState.IsValid)
        {
            if (testType.Id == Guid.Empty)
            {
                _testTypeService.AddTestType(testType);

                TempData["Success"] = "Test Type Added Successfully";
            }
            else
            {
                _testTypeService.UpdateTestType(testType);

                TempData["Info"] = "Test Type Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        return View(testType);

    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteTestType(Guid id)
    {
        var testType = _unitOfWork.TestType.GetFirstOrDefault(x => x.Id == id);

        if (ModelState.IsValid)
        {
            if (testType != null)
            {
                _testTypeService.DeleteTestType(testType);

                TempData["Delete"] = "Test Type Delete Successfully";
                
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        return View(testType);
    }
    #endregion
}
