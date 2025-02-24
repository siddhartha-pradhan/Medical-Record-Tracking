using Microsoft.AspNetCore.Mvc;
using EMR.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using EMR.Application.Interfaces.Services;
using EMR.Infrastructure.Implementation.Services;
using EMR.Core.ViewModels;
using ChartJSCore.Models;
using ChartJSCore.Helpers;
using ChartJSCore.Plugins.Zoom;

namespace EMR.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Constants.Admin)]
public class HomeController : Controller
{
	private readonly IAppUserService _appUserService;
	private readonly IPatientService _patientService;
	private readonly IDoctorService _doctorService;
	private readonly IAppointmentService _appointmentService;
	private readonly ISpecialtyService _specialtyService;
	private readonly ITestService _testService;
	private readonly IMedicineService _medicineService;

	public HomeController(IAppUserService appUserService,
		IPatientService patientService,
		IDoctorService doctorService,
		IAppointmentService appointmentService,
		ISpecialtyService specialtyService,
		ITestService testService,
		IMedicineService medicineService)
	{
		_appUserService = appUserService;
		_patientService = patientService;
		_doctorService = doctorService;
		_appointmentService = appointmentService;
		_specialtyService = specialtyService;
		_testService = testService;
		_medicineService = medicineService;
	}

	public IActionResult Index()
	{
		var patientCount = (from user in _appUserService.GetAllUsers().Where(x => x.EmailConfirmed)
							join patient in _patientService.GetAllPatients()
							 on user.Id equals patient.UserId
							select patient).ToList().Count();

		var doctorCount = (from user in _appUserService.GetAllUsers()
						   join doctor in _doctorService.GetAllDoctors().Where(x => x.IsApproved)
							 on user.Id equals doctor.UserId
						   select doctor).ToList().Count();

		var diagnosticsCount = _testService.GetAllDiagnosticTests().Count();

		var medicineCount = _medicineService.GetAllMedicines().Count();

		

		Chart verticalBarChart = GenerateVerticalBarChart();
		Chart lineChart = GenerateLineChart();

		ViewData["VerticalBarChart"] = verticalBarChart;
		ViewData["LineChart"] = lineChart;

		var adminPanel = new AdminViewModel()
		{
			DiagnosticCount = diagnosticsCount,
			DoctorCount = doctorCount,
			MedicationCount = medicineCount,
			PatientCount = patientCount
		};

		return View(adminPanel);
	}

	private Chart GenerateVerticalBarChart()
	{
		var appointments = (from appointment in _appointmentService.GetAllListAppointments()
							join doctor in _doctorService.GetAllDoctors()
							 on appointment.DoctorId equals doctor.Id
							join user in _appUserService.GetAllUsers()
							 on doctor.UserId equals user.Id
							group appointment by new { doctor.Id, user.FullName } into g
							select new DoctorAppointmentViewModel
							{
								Name = g.Key.FullName,
								TotalAppointments = g.Count()
							}).ToList();

		Chart chart = new Chart();

		chart.Type = Enums.ChartType.Bar;

		Data data = new Data();

		data.Labels = new List<string>();

		foreach (var item in appointments)
		{
			data.Labels.Add(item.Name);
		}

		var counts = new List<double?>();

		foreach (var item in appointments)
		{
			counts.Add(item.TotalAppointments);
		}

		BarDataset dataset = new BarDataset()
		{
			Label = "Appointment Count",
			Data = counts,
			BackgroundColor = new List<ChartColor>
				{
					ChartColor.FromRgba(255, 99, 132, 0.2),
				},
			BorderColor = new List<ChartColor>
				{
					ChartColor.FromRgb(255, 99, 132),
				},
			BorderWidth = new List<int>() { 1 },
			BarPercentage = 0.5,
			BarThickness = 6,
			MaxBarThickness = 8,
			MinBarLength = 2
		};

		data.Datasets = new List<Dataset>();
		data.Datasets.Add(dataset);

		chart.Data = data;

		var options = new Options
		{
			Scales = new Dictionary<string, Scale>()
				{
					{ "y", new CartesianLinearScale()
						{
							BeginAtZero = true
						}
					},
					{ "x", new Scale()
						{
							Grid = new Grid()
							{
								Offset = true
							}
						}
					},
				}
		};

		chart.Options = options;

		chart.Options.Layout = new Layout
		{
			Padding = new Padding
			{
				PaddingObject = new PaddingObject
				{
					Left = 10,
					Right = 12
				}
			}
		};

		return chart;
	}

	private Chart GenerateLineChart()
	{
		var appointments = (from appointment in _appointmentService.GetAllListAppointments()
							join doctor in _doctorService.GetAllDoctors()
							 on appointment.DoctorId equals doctor.Id
							join specialty in _specialtyService.GetAllSpecialties()
							 on doctor.DepartmentId equals specialty.Id
							group appointment by specialty.Name  into g
							select new SpecialtyViewModel
							{
								Name = g.Key,
								Count = g.Count()
							}).ToList();

		var specialtyData = new List<string>();
		var specialtyCount = new List<double?>();

		foreach (var brand in appointments)
		{
			specialtyData.Add(brand.Name);
			specialtyCount.Add(brand.Count);
		}

		Chart chart = new Chart();

		chart.Type = Enums.ChartType.Line;
		chart.Options.Scales = new Dictionary<string, Scale>();
		CartesianScale xAxis = new CartesianScale();
		xAxis.Display = true;
		xAxis.Title = new Title
		{
			Text = new List<string> { "Specialty Appointment Count" },
			Display = true
		};
		chart.Options.Scales.Add("x", xAxis);

		Data data = new Data
		{
			Labels = specialtyData
		};

		LineDataset dataset = new LineDataset()
		{
			Label = "Specialty Appointments",
			Data = specialtyCount,
			Fill = "true",
			Tension = .01,
			BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(75, 192, 192, 0.4) },
			BorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
			BorderCapStyle = "butt",
			BorderDash = new List<int>(),
			BorderDashOffset = 0.0,
			BorderJoinStyle = "miter",
			PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
			PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
			PointBorderWidth = new List<int> { 1 },
			PointHoverRadius = new List<int> { 5 },
			PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
			PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
			PointHoverBorderWidth = new List<int> { 2 },
			PointRadius = new List<int> { 1 },
			PointHitRadius = new List<int> { 10 },
			SpanGaps = false
		};

		data.Datasets = new List<Dataset>
			{
				dataset
			};

		chart.Data = data;

		ZoomOptions zoomOptions = new ZoomOptions
		{
			Zoom = new Zoom
			{
				Wheel = new Wheel
				{
					Enabled = true
				},
				Pinch = new Pinch
				{
					Enabled = true
				},
				Drag = new Drag
				{
					Enabled = true,
					ModifierKey = Enums.ModifierKey.alt
				}
			},
			Pan = new Pan
			{
				Enabled = true,
				Mode = "xy"
			}
		};

		chart.Options.Plugins = new ChartJSCore.Models.Plugins
		{
			PluginDynamic = new Dictionary<string, object> { { "zoom", zoomOptions } }
		};

		return chart;

	}

}
