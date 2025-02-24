using EMR.Core.Entities;

namespace EMR.Core.ViewModels;

public class CartViewModel
{
    public TestCart TestCart { get; set; }

    public List<TestTypeViewModel> TestTypes { get; set; }
}
