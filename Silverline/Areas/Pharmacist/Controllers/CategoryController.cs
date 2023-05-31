using Microsoft.AspNetCore.Mvc;
using Silverline.Core.Entities;
using Silverline.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Areas.Pharmacist.Controllers;

[Area("Pharmacist")]
[Authorize(Roles = Constants.Pharmacist)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryService _categoryService;

    public CategoryController(IUnitOfWork unitOfWork, 
        ICategoryService categoryService)
    {
        _unitOfWork = unitOfWork;
        _categoryService = categoryService;
    }

    #region Razor Pages
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(Guid? id)
    {
        var category = new Category();

        if(id == null)
        {
            return View(category);
        }

        category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

        if(category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    public IActionResult Delete(Guid id)
    {
        var category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }
    #endregion

    #region API Calls
    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = _categoryService.GetAllCategories();

        return Json(new { data = categories });
    }

    [HttpPost, ActionName("Upsert")]
    public IActionResult UpsertCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            if(category.Id == Guid.Empty)
            {
                _categoryService.AddCategory(category);

                TempData["Success"] = "Category Added Successfully";
            }
            else
            {
                _categoryService.UpdateCategory(category);
                
                TempData["Info"] = "Category Updated Successfully";
            }
            
            return RedirectToAction("Index");
        }

        return View(category);

    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteCategory(Guid id)
    {
        var category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
        
        if (ModelState.IsValid)
        {
            if (category != null)
            {
                _categoryService.DeleteCategory(category);
                TempData["Delete"] = "Category Delete Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        return View(category);
    }
    #endregion
}
