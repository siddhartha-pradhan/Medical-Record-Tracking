using EMR.Application.Interfaces.Repositories;
using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;

namespace EMR.Infrastructure.Implementation.Repositories;

public class AppointmentDetailRepository : Repository<AppointmentDetail>, IAppointmentDetailRepository
{
	private readonly ApplicationDbContext _dbContext;

	public AppointmentDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
