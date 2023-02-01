using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Implementation.Repositories;
using Silverline.Infrastructure.Implementation.Services;

namespace Silverline.Areas.Admin.Controllers
{
 
    [Area("Admin")]
    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly ISpecialtyService _specialtyService;

        public SpecialtyController(ISpecialtyRepository specialtyRepository, ISpecialtyService specialtyService)
        {
            _specialtyRepository = specialtyRepository;
            _specialtyService = specialtyService;
        }

        #region Razor Pages
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(Guid? id)
        {
            var specialty = new Specialty();

            if (id == null)
            {
                return View();
            }

            specialty = _specialtyRepository.GetFirstOrDefault(x => x.Id == id);

            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }

        public IActionResult Delete(Guid id)
        {
            var specialty = _specialtyRepository.GetFirstOrDefault(x => x.Id == id);

            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }
        #endregion

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var specialties = _specialtyService.GetAllSpecialties();
            return Json(new { data = specialties });
        }

        [HttpPost, ActionName("Upsert")]
        public IActionResult UpsertSpecialty(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                if (specialty.Id == Guid.Parse(""))
                {
                    _specialtyService.AddSpecialty(specialty);
                    TempData["Success"] = "Specialty Added Successfully";
                }
                else
                {
                    _specialtyService.UpdateSpecialty(specialty);
                    TempData["Info"] = "Specialty Updated Successfully";
                }

                return RedirectToAction("Index");
            }

            return View(specialty);

        }

        [HttpDelete, ActionName("Delete")]
        public IActionResult DeleteSpecialty(Guid id)
        {
            var specialty = _specialtyRepository.GetFirstOrDefault(x => x.Id == id);

            if (ModelState.IsValid)
            {
                if (specialty != null)
                {
                    _specialtyService.DeleteSpecialty(specialty);
                    TempData["Delete"] = "Specialty Delete Successfully";
                }
                else
                {
                    return NotFound();
                }
            }

            return View(specialty);
        }
        #endregion
    }
}
