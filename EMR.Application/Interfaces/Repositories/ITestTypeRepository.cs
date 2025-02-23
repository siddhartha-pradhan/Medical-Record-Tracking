using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Repositories;

public interface ITestTypeRepository : IRepository<TestType>
{
    void Update(TestType testType);

    void Delete(TestType testType);
}
