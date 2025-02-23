using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;

namespace Silverline.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = Constants.Doctor)]
public class MedicineController : Controller
{
	private readonly IMedicineService _medicineService;
	private readonly ICategoryService _categoryService;
	private readonly IManufacturerService _manufacturerService;

	public MedicineController(IMedicineService medicineService, 
		ICategoryService categoryService, 
		IManufacturerService manufacturerService)
	{
		_medicineService = medicineService;
		_categoryService = categoryService;
		_manufacturerService = manufacturerService;
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpGet]
	public IActionResult GetAll()
	{
		var medicines = _medicineService.GetAllMedicines();
		var categories = _categoryService.GetAllCategories();
		var manufacturers = _manufacturerService.GetAllManufacturerers();

		var result = from medicine in medicines
					 join category in categories
					 on medicine.CategoryId equals category.Id
					 join manufacturer in manufacturers
					 on medicine.ManufacturerId equals manufacturer.Id
					 select new
					 {
						 Id = medicine.Id,
						 Name = medicine.Name,
						 Description = medicine.Description,
						 UnitPrice = medicine.UnitPrice,
						 Type = medicine.Type,
						 Category = category.Name,
						 Manufacturer = manufacturer.Name,
					 };

		return Json(new { data = result });
	}
}
