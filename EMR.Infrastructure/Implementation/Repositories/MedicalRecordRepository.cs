using EMR.Application.Interfaces.Repositories;
using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Infrastructure.Implementation.Repositories;

public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MedicalRecordRepository(ApplicationDbContext dbContext) : base (dbContext)
	{
            
	}

}
