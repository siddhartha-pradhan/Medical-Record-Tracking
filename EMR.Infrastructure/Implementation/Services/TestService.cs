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
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddDiagnosticTest(DiagnosticTest test)
        {
            _unitOfWork.Test.Add(test);
            _unitOfWork.Save();
        }

        public void DeleteDiagnosticTest(DiagnosticTest test)
        {
            _unitOfWork.Test.Delete(test);
            _unitOfWork.Save();
        }

        public List<DiagnosticTest> GetAllDiagnosticTests()
        {
            return _unitOfWork.Test.GetAll();
        }

        public DiagnosticTest GetDiagnosticTest(Guid Id)
        {
            return _unitOfWork.Test.Get(Id);
        }

        public void UpdateDiagnosticTest(DiagnosticTest test)
        {
            _unitOfWork.Test.Update(test);
            _unitOfWork.Save();
        }
    }
}
