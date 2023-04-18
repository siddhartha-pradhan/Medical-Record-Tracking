using Silverline.Application.Interfaces.Services;
using Silverline.Core.Entities;

namespace Silverline.UnitTest;

public class TestCartService : ITestCartService
{
	private readonly List<TestCart> _carts;

	public TestCartService()
	{
		_carts = new List<TestCart>()
		{
			new TestCart()
			{
				Id = Guid.NewGuid(),
				PatientId = Guid.NewGuid(),
				TestId = Guid.NewGuid(),
			},
			new TestCart()
			{
				Id = Guid.NewGuid(),
				PatientId = Guid.NewGuid(),
				TestId = Guid.NewGuid(),
			},
			new TestCart()
			{
				Id = Guid.NewGuid(),
				PatientId = Guid.NewGuid(),
				TestId = Guid.NewGuid(),
			},
		};
	}

	public void AddTest(TestCart testCart)
	{
		testCart.Id = Guid.NewGuid();
		_carts.Add(testCart);
	}

	public List<TestCart> GetAllTestCarts()
	{
		return _carts;
	}

	public TestCart GetTestCart(Guid Id)
	{
		return _carts.FirstOrDefault(x => x.Id == Id);
	}

	public void Remove(TestCart testCart)
	{
		var item = _carts.FirstOrDefault(a => a.Id == testCart.Id);
		_carts.Remove(item);
	}
}
