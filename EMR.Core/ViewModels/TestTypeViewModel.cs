using System.ComponentModel.DataAnnotations;

namespace EMR.Core.ViewModels;

public class TestTypeViewModel
{
    public string Id { get; set; }

    [Display(Name = "Test Title")]
    public string Title { get; set; }
    
    public string Unit { get; set; }
    
    [Display(Name = "Initial Range")]
    public float InitialRange { get; set; }
    
    [Display(Name = "Final Range")]
    public float FinalRange { get; set; }
    
    [Display(Name = "Unit Price")]
    public float UnitPrice { get; set; }

    [Display(Name = "Test Type")]
    public string TestType { get; set; }
}
