using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface IMedicineService
{
    Medicine GetMedicine(Guid Id);

    List<Medicine> GetAllMedicines();

    void AddMedicine(Medicine medicine);

    void UpdateMedicine(Medicine medicine);

    void DeleteMedicine(Medicine medicine);
}
