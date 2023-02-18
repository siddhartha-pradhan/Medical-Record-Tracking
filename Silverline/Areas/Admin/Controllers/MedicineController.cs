using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Constants;
using Silverline.Core.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class MedicineController : Controller
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly IMedicineService _medicineService;
    private readonly ICategoryService _categoryService;
    private readonly IManufacturerService _manufacturerService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MedicineController(IMedicineRepository medicineRepository, IMedicineService medicineService, IWebHostEnvironment webHostEnvironment)
    {
        _medicineRepository = medicineRepository;
        _medicineService = medicineService;
        _webHostEnvironment = webHostEnvironment;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(Guid? id)
    {
        var medicineViewModel = new MedicineViewModel()
        {
            Medicine = new(),
            Categories = _categoryService.GetAllCategories()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
            Manufacturerers = _manufacturerService.GetAllManufacturerers() 
                .Select(x => new SelectListItem 
                { 
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
        };

        if(id == null)
        {
            return View(medicineViewModel);
        }

        var medicine = medicineViewModel.Medicine = _medicineRepository.GetFirstOrDefault(x => x.Id == id);

        if(medicine == null) 
        {
            return NotFound();
        }

        return View(medicineViewModel);
    }

    public IActionResult Delete(Guid id)
    {
        var medicineViewModel = new MedicineViewModel();
        
        var medicine = medicineViewModel.Medicine = _medicineRepository.GetFirstOrDefault(x => x.Id == id);

        if (medicine == null)
        {
            return NotFound();
        }

        return View(medicineViewModel);
    }
    #endregion

    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        var medicines = _medicineService.GetAllMedicines();
        return Json(new { data = medicines });
    }

    [HttpPost, ActionName("Upsert")]
    public IActionResult UpsertMedicine(MedicineViewModel medicineViewModel, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;

            if(image != null)
            {
                var fileName = Guid.NewGuid().ToString();

                var uploads = Path.Combine(wwwRootPath, @"images\medicines");

                var extension = Path.GetExtension(image.FileName);

                if (medicineViewModel.Medicine.ImageURL != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, medicineViewModel.Medicine.ImageURL.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Exists(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    image.CopyTo(fileStreams);
                }

                medicineViewModel.Medicine.ImageURL = @"\images\medicines\" + fileName + extension;
            }

            var medicine = medicineViewModel.Medicine;

            if (medicine.Id == Guid.Empty)
            {
                _medicineService.AddMedicine(medicine);
                TempData["Success"] = "Medicine Added Successfully";
            }
            else
            {
                _medicineService.UpdateMedicine(medicine);
                TempData["Info"] = "Medicine Updated Successfully";
            }
            
            return RedirectToAction("Index");

        }

        return View(medicineViewModel);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteMedicine(Guid id)
    {
        var medicine = _medicineRepository.GetFirstOrDefault(u => u.Id == id);

        if (medicine != null)
        {
            _medicineService.DeleteMedicine(medicine);
            TempData["Delete"] = "Medicine Deleted Successfully";
            return RedirectToAction("Index");
        }
        else
        {
            return NotFound();
        }
    }
    #endregion
}
