using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Repositories;

public interface ITestRepository : IRepository<DiagnosticTest>
{
    void Update(DiagnosticTest diagnosticTest);

    void Delete(DiagnosticTest diagnosticTest);
}
