using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Infrastructure.Implementation.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Add(Category category)
        {
            category.CreatedAt = DateTime.Now;
            base.Add(category);
        }

        public void Update(Category category)
        {
            var item = _dbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
            
            if (item != null) 
            {
                item.Name = category.Name;
                item.LastModifiedAt = DateTime.Now;
            }
        }
    }
}
