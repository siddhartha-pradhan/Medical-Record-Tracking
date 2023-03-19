using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MedicalRecordRepository(ApplicationDbContext dbContext) : base (dbContext)
	{
            
	}

}
