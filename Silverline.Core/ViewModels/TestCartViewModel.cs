using Silverline.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.ViewModels;

public class TestCartViewModel
{
    public TestHeader TestHeader { get; set; }

    [Display(Name = "Pay via Credits")]
    public string PaymentStatus { get; set; }

    public List<TestCart> TestCartList { get; set; }    
}
