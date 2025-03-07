﻿using Microsoft.AspNetCore.Mvc;
using EMR.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using EMR.Application.Interfaces.Repositories;
using EMR.Application.Interfaces.Services;

namespace EMR.Areas.Doctor.Controllers;

[Area("Doctor")]
[Authorize(Roles = Constants.Doctor)]
public class LabTestController : Controller
{
    private readonly ITestService _testService;
    private readonly ITestTypeService _testTypeService;

    public LabTestController(ITestService testService,
        ITestTypeService testTypeService)
    {
        _testService = testService;
        _testTypeService = testTypeService;
    }

    public IActionResult Index()
    {
        var tests = _testService.GetAllDiagnosticTests();

        var testTypes = _testTypeService.GetAllTestTypes();

        var result = (from test in tests
                      join testType in testTypes
                      on test.ClassId equals testType.Id
                      select new
                      {
                          Id = test.Id,
                          Title = test.Title,
                          InitialRange = test.InitialRange,
                          FinalRange = test.FinalRange,
                          Unit = test.Unit,
                          UnitPrice = test.UnitPrice,
                          TestType = testType.Name
                      }).ToList();

        return View(tests);
    }
}
