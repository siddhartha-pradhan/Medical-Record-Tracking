using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    private readonly ApplicationDbContext _dbContext;

	public AppointmentRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
