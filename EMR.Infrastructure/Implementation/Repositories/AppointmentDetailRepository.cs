using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class AppointmentDetailRepository : Repository<AppointmentDetail>, IAppointmentDetailRepository
{
	private readonly ApplicationDbContext _dbContext;

	public AppointmentDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
