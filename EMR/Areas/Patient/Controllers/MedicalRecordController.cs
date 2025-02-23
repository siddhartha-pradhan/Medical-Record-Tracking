using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Implementation.Services;
using System.Configuration;
using System.Security.Claims;

namespace Silverline.Areas.Patient.Controllers;

[Area("Patient")]
public class MedicalRecordController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IPatientService _patientService;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IAppUserService _appUserService;

    public MedicalRecordController(IMedicalRecordService medicalRecordService, IPatientService patientService, IAppUserService appUserService, IWebHostEnvironment webHostEnvironment)
    {
        _medicalRecordService = medicalRecordService;
        _webHostEnvironment = webHostEnvironment;
        _patientService = patientService;
        _appUserService = appUserService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Export()
    {
        var fileName = ExcelTemplate();
        var filePath = string.Format("{0}/{1}", _webHostEnvironment.WebRootPath, fileName);
        var excelFile = $"Record Template.xlsx";
        var streamFile = System.IO.File.OpenRead(filePath);
        return new FileStreamResult(streamFile, "application/vnd.ms-excel") { FileDownloadName = excelFile };
    }

    private static string ExcelTemplate()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configurations = configuration.Build();

        var excelURL = configurations.GetSection("ExcelTemplate");

        return "/excel/records.xlsx";
    }

    [HttpPost]
    public IActionResult AddRecords(List<MedicalRecord> medicalRecords)
    {
        foreach (var item in medicalRecords)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            var patient = _patientService.GetAllPatients().Where(x => x.UserId == claim.Value).FirstOrDefault();

            item.PatientId = patient.Id;

            _medicalRecordService.AddMedicalRecord(item);
        }

        return RedirectToAction("Index");
    }
}
