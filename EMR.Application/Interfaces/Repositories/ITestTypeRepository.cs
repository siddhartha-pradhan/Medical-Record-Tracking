using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Repositories;

public interface ITestTypeRepository : IRepository<TestType>
{
    void Update(TestType testType);

    void Delete(TestType testType);
}
