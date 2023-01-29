using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface ITestTypeService
{
    TestType GetTestType(Guid Id);

    List<TestType> GetAllTestTypes();

    void AddTestType(TestType testType);

    void UpdateTestType(TestType testType);

    void DeleteTestType(TestType testType);
}
