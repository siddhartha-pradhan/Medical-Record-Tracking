using Silverline.Core.Entities;

namespace Silverline.Core.ViewModels;

public class CartViewModel
{
    public TestCart TestCart { get; set; }

    public List<TestTypeViewModel> TestTypes { get; set; }
}
