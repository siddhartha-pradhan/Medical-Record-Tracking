using EMR.Core.Constants;
using EMR.Core.ViewModels.MLModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMR.Areas.Patient.Controllers;

[Area("Patient")]
[Authorize(Roles = Constants.Patient)]
public class PredictionController(IWebHostEnvironment webHostEnvironment) : Controller
{
    [HttpGet]
    public IActionResult HeartDiseasePredict()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult HeartDiseasePredict(HeartData input)
    {
        var model = new MLModel(webHostEnvironment);
        model.Build();
        var result = model.Consume(input);
        ViewBag.HeartPrediction = result;
        return View();
    }
    
    [HttpGet]
    public IActionResult DiseasePrediction()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult DiseasePrediction(SymptomDataWithLabel input)
    {
        var model = new MLModel(webHostEnvironment);
        var result = model.TrainAndSaveModel(input);
        ViewBag.DiseasePrediction = result;
        return View();
    }
}