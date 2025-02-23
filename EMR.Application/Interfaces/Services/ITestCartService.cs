using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface ITestCartService
{
    List<TestCart> GetAllTestCarts();

    TestCart GetTestCart(Guid Id);

    void AddTest(TestCart testCart);

    void Remove(TestCart testCart);

    void Delete(Guid patientId, Guid testId);

    void Update(TestCart cart);

    void Finalize(TestCart cart);

    void Cancel(Guid cartId);


}
