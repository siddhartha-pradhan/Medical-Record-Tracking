using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Infrastructure.Implementation.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCategory(Category category)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
        }

        public void DeleteCategory(Category category)
        {
            _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
        }

        public List<Category> GetAllCategories()
        {
            return _unitOfWork.Category.GetAll();
        }

        public Category GetCategory(Guid Id)
        {
            return _unitOfWork.Category.Get(Id);
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.Category.Update(category);   
            _unitOfWork.Save();
        }
    }
}
