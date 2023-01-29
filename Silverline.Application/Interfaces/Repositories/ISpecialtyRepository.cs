using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Repositories;

public interface ISpecialtyRepository : IRepository<Specialty>
{
    void Update(Specialty specialty);

    void Delete(Specialty specialty);

}
