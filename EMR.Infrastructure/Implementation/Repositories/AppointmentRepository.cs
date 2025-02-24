using EMR.Infrastructure.Persistence;
using EMR.Application.Interfaces.Repositories;
using EMR.Core.Entities;

namespace EMR.Infrastructure.Implementation.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    private readonly ApplicationDbContext _dbContext;

	public AppointmentRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
