using Silverline.Core.Entities;
using Silverline.Application.Interfaces.Services;
using Silverline.Infrastructure.Implementation.Repositories;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Constants;

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

    public void Delete(Guid patientId, Guid testId)
    {
        var result = _unitOfWork.TestCart.GetAll().Where(x => x.PatientId == patientId && x.TestId == testId).ToList();

        foreach(var item in result)
        {
            _unitOfWork.TestCart.Remove(item);
            _unitOfWork.Save();
        }
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

    public void Update(TestCart cart)
    {
        var result = _unitOfWork.TestCart.Get(cart.Id);

        if(cart != null)
        {
            result.ActionStatus = cart.ActionStatus;
            result.PaymentStatus = cart.PaymentStatus;
            result.BookedDate = DateTime.Now;
        }

        _unitOfWork.Save();
    }

    public void Finalize(TestCart cart)
    {
        var result = _unitOfWork.TestCart.Get(cart.Id);

        if (cart != null)
        {
            result.Value = cart.Value;
            result.PaymentStatus = Constants.Completed;
            result.FinalizedDate = DateTime.Now;
            result.ActionStatus = cart.ActionStatus;
            result.TechnicianRemarks = cart.TechnicianRemarks;
        }

        _unitOfWork.Save();
    }

    public void Cancel(Guid cartId)
    {
        var result = _unitOfWork.TestCart.Get(cartId);
        if (result != null)
        {
            _unitOfWork.TestCart.Remove(result);
			_unitOfWork.Save();
		}
	}

}
