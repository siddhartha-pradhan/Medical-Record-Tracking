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
    public class TestTypeService : ITestTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddTestType(TestType testType)
        {
            _unitOfWork.TestType.Add(testType);
            _unitOfWork.Save();
        }

        public void DeleteTestType(TestType testType)
        {
            _unitOfWork.TestType.Delete(testType);
            _unitOfWork.Save();
        }

        public List<TestType> GetAllTestTypes()
        {
            return _unitOfWork.TestType.GetAll();
        }

        public TestType GetTestType(Guid Id)
        {
            return _unitOfWork.TestType.Get(Id);
        }

        public void UpdateTestType(TestType testType)
        {
            _unitOfWork.TestType.Update(testType);
            _unitOfWork.Save();
        }
    }
}
