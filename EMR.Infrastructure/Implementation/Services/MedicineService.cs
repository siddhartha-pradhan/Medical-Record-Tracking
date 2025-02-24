using EMR.Application.Interfaces.Repositories;
using EMR.Application.Interfaces.Services;
using EMR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Infrastructure.Implementation.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddMedicine(Medicine medicine)
        {
            _unitOfWork.Medicine.Add(medicine);
            _unitOfWork.Save();
        }

        public void DeleteMedicine(Medicine medicine)
        {
            _unitOfWork.Medicine.Delete(medicine);
            _unitOfWork.Save();
        }

        public List<Medicine> GetAllMedicines()
        {
            return _unitOfWork.Medicine.GetAll();
        }

        public Medicine GetMedicine(Guid Id)
        {
            return _unitOfWork.Medicine.Get(Id);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            _unitOfWork.Medicine.Update(medicine);
            _unitOfWork.Save();
        }
    }
}
