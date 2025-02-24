namespace EMR.Core.ViewModels;

public class DeleteMedicineViewModel
{
    public Guid Id { get; set; }    

    public string Name { get; set; }

    public string Description { get; set; }

    public float UnitPrice { get; set; }

    public string Type { get; set; }

    public string Category { get; set; }

    public string Manufacturer { get; set; }
}
