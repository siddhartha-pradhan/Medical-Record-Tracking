using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable  
    {
        ICategoryRepository Category { get; set; }

        IManufacturerRepository Manufacturer { get; set; }

        IMedicineRepository Medicine { get; set; }

        ISpecialtyRepository Specialty { get; set; }

        ITestRepository Test { get; set; }

        ITestTypeRepository TestType { get; set; }

        void Save();
    }
}
