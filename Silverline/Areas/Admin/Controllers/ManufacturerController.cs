using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Entities;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class ManufacturerController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IManufacturerService _manufacturerService;

    public ManufacturerController(IUnitOfWork unitOfWork, IManufacturerService manufacturerService)
    {
        _unitOfWork = unitOfWork;
        _manufacturerService = manufacturerService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(Guid? id) 
    {
        var manufacturer = new Manufacturer();

        if (id == null)
        {
            return View(manufacturer);
        }

        manufacturer = _unitOfWork.Manufacturer.GetFirstOrDefault(x => x.Id == id);

        if (manufacturer == null)
        {
            return NotFound();
        }

        return View(manufacturer);
    }

    public IActionResult Delete(Guid id) 
    {
        var manufacturer = _unitOfWork.Manufacturer.GetFirstOrDefault(x => x.Id == id);

        if (manufacturer == null)
        {
            return NotFound();
        }

        return View(manufacturer);
    }
    #endregion

    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        var manufacturers = _manufacturerService.GetAllManufacturerers();
        return Json(new { data = manufacturers });
    }

    [HttpPost, ActionName("Upsert")]
    public IActionResult UpsertManufacturer(Manufacturer manufacturer)
    {
        if (ModelState.IsValid)
        {
            if (manufacturer.Id == Guid.Empty)
            {
                _manufacturerService.AddManufacturer(manufacturer);
                TempData["Success"] = "Manufacturer Added Successfully";
            }
            else
            {
                _manufacturerService.UpdateManufacturer(manufacturer);
                TempData["Info"] = "Manufacturer Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        return View(manufacturer);
    }

    [HttpPost]
    public IActionResult UpdateManufacturer(Guid id)
    {
        var manufacturer = _unitOfWork.Manufacturer.GetFirstOrDefault(x => x.Id == id);

        if (ModelState.IsValid)
        {
            _manufacturerService.UpdateStatusManufacturer(manufacturer);
            TempData["Info"] = "Activation Status Updated Successfully";
            return Json(new { success = true, message = "Successful operation" });
        }

        return View(manufacturer);

    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteManufacturer(Guid id)
    {
        var manufacturer = _unitOfWork.Manufacturer.GetFirstOrDefault(x => x.Id == id);

        if (ModelState.IsValid)
        {
            if (manufacturer != null)
            {
                _manufacturerService.DeleteManufacturer(manufacturer);
                TempData["Delete"] = "Manufacturer Delete Successfully";
            }
            else
            {
                return NotFound();
            }
        }

        return View(manufacturer);
    }
    #endregion
}
