using Silverline.Core.Entities;
using Silverline.Application.Interfaces.Services;
using Silverline.Infrastructure.Implementation.Repositories;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Services;

public class TestCartService : ITestCartService
{
	private readonly IUnitOfWork _unitOfWork;
	public TestCartService(IUnitOfWork unitOfWork)
	{
        _unitOfWork = unitOfWork; 
    }

    public void AddTest(TestCart testCart)
    {
        _unitOfWork.TestCart.Add(testCart);
        _unitOfWork.Save();
    }

    public List<TestCart> GetAllTestCarts()
    {
        return _unitOfWork.TestCart.GetAll();
    }

    public TestCart GetTestCart(Guid Id)
    {
        return _unitOfWork.TestCart.GetFirstOrDefault(x => x.Id == Id);
    }

    public void Remove(TestCart testCart)
    {
        _unitOfWork.TestCart.Remove(testCart); 
        _unitOfWork.Save();
    }
}
