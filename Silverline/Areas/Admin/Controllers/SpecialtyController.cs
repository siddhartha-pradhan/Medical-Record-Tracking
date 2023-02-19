using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Entities;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class SpecialtyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpecialtyService _specialtyService;

	public SpecialtyController(IUnitOfWork unitOfWork, ISpecialtyService specialtyService)
	{
        _unitOfWork = unitOfWork;
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
			return View(specialty);
		}

		specialty = _unitOfWork.Specialty.GetFirstOrDefault(x => x.Id == id);

		if (specialty == null)
		{
			return NotFound();
		}

		return View(specialty);
	}

	public IActionResult Delete(Guid id)
	{
		var specialty = _unitOfWork.Specialty.GetFirstOrDefault(x => x.Id == id);

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
			if (specialty.Id == Guid.Empty)
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

	[HttpPost, ActionName("Delete")]
	public IActionResult DeleteSpecialty(Guid id)
	{
		var specialty = _unitOfWork.Specialty.GetFirstOrDefault(x => x.Id == id);

		if (ModelState.IsValid)
		{
			if (specialty != null)
			{
				_specialtyService.DeleteSpecialty(specialty);
				TempData["Delete"] = "Specialty Delete Successfully";
				return RedirectToAction("Index");
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
