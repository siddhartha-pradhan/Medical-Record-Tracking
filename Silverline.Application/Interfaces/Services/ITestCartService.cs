using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface ITestCartService
{
    List<TestCart> GetAllTestCarts();

    TestCart GetTestCart(Guid Id);

    void AddTest(TestCart testCart);

    void Remove(TestCart testCart);
}
