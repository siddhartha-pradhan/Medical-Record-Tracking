using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Implementation.Repositories;
using Silverline.Infrastructure.Implementation.Services;

namespace Silverline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestTypeController : Controller
    {
        private readonly ITestTypeRepository _testTypeRepository;
        private readonly ITestTypeService _testTypeService;

        public TestTypeController(ITestTypeRepository testTypeRepository, ITestTypeService testTypeService)
        {
            _testTypeRepository = testTypeRepository;
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
                return View();
            }

            testType = _testTypeRepository.GetFirstOrDefault(x => x.Id == id);

            if (testType == null)
            {
                return NotFound();
            }

            return View(testType);
        }

        public IActionResult Delete(Guid id)
        {
            var testType = _testTypeRepository.GetFirstOrDefault(x => x.Id == id);

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
                if (testType.Id == Guid.Parse(""))
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

        [HttpDelete, ActionName("Delete")]
        public IActionResult DeleteTestType(Guid id)
        {
            var testType = _testTypeRepository.GetFirstOrDefault(x => x.Id == id);

            if (ModelState.IsValid)
            {
                if (testType != null)
                {
                    _testTypeService.DeleteTestType(testType);
                    TempData["Delete"] = "Test Type Delete Successfully";
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
}
