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
    public class ManufacturerService : IManufacturerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManufacturerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            _unitOfWork.Manufacturer.Add(manufacturer);
            _unitOfWork.Save();
        }

        public void DeleteManufacturer(Manufacturer manufacturer)
        {
            _unitOfWork.Manufacturer.Delete(manufacturer);
            _unitOfWork.Save();
        }

        public List<Manufacturer> GetAllManufacturerers()
        {
            return _unitOfWork.Manufacturer.GetAll();
        }

        public Manufacturer GetManufacturer(Guid Id)
        {
            return _unitOfWork.Manufacturer.Get(Id);
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            _unitOfWork.Manufacturer.Update(manufacturer);
            _unitOfWork.Save();
        }
    }
}
