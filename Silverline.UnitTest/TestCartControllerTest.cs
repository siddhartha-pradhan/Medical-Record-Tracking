using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Areas.Patient.Controllers;
using Silverline.Infrastructure.Implementation.Repositories;
using Silverline.Infrastructure.Implementation.Services;
using Silverline.Infrastructure.Persistence;

namespace Silverline.UnitTest;

public class TestCartControllerTest
{
	private readonly CartController _testController;
	private readonly ApplicationDbContext _dbContext;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ITestCartService _testCartService;
	private readonly ITestService _testService;
	private readonly IPatientService _patientService;

	public TestCartControllerTest()
	{
		_unitOfWork = new UnitOfWork(_dbContext);
		_testCartService = new TestCartService();
		_patientService = new PatientService(_unitOfWork);
		_testService = new TestService(_unitOfWork);
		_testController = new CartController(_testCartService, _patientService, _testService);
	}

	[Test]
	public void GetWhenCalledReturnsOkResult()
	{
		var okResult = _testController.Index();

		//Assert.IsTrue<OkObjectResult>(okResult as OkObjectResult);
	}
}
