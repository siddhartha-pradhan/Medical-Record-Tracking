using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface ITestService
{
    DiagnosticTest GetDiagnosticTest(Guid Id);

    List<DiagnosticTest> GetAllDiagnosticTests();

    void AddDiagnosticTest(DiagnosticTest test);

    void UpdateDiagnosticTest(DiagnosticTest test);

    void DeleteDiagnosticTest(DiagnosticTest test);
}
