using Microsoft.AspNetCore.Mvc;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Entities;

namespace Silverline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;

        public CategoryController(IUnitOfWork unitOfWork, ICategoryService categoryService)
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
                return View();
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
                if(category.Id == Guid.Parse(""))
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

        [HttpDelete, ActionName("Delete")]
        public IActionResult DeleteCategory(Guid id)
        {
            var category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            
            if (ModelState.IsValid)
            {
                if (category != null)
                {
                    _categoryService.DeleteCategory(category);
                    TempData["Delete"] = "Category Delete Successfully";
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
}
