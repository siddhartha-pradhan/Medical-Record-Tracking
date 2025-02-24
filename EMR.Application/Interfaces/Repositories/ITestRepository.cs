using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Repositories;

public interface ITestRepository : IRepository<DiagnosticTest>
{
    void Update(DiagnosticTest diagnosticTest);

    void Delete(DiagnosticTest diagnosticTest);
}
