using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface ITestTypeService
{
    TestType GetTestType(Guid Id);

    List<TestType> GetAllTestTypes();

    void AddTestType(TestType testType);

    void UpdateTestType(TestType testType);

    void DeleteTestType(TestType testType);
}
