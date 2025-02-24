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
    public class SpecialtyService : ISpecialtyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecialtyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddSpecialty(Specialty specialty)
        {
            _unitOfWork.Specialty.Add(specialty);
            _unitOfWork.Save();
        }

        public void DeleteSpecialty(Specialty specialty)
        {
            _unitOfWork.Specialty.Delete(specialty);
            _unitOfWork.Save();
        }

        public List<Specialty> GetAllSpecialties()
        {
            return _unitOfWork.Specialty.GetAll();
        }

        public Specialty GetSpecialty(Guid Id)
        {
            return _unitOfWork.Specialty.Get(Id);
        }

        public void UpdateSpecialty(Specialty specialty)
        {
            _unitOfWork.Specialty.Update(specialty);
            _unitOfWork.Save();
        }
    }
}
