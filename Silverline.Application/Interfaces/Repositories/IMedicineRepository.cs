﻿using Silverline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Application.Interfaces.Repositories
{
    public interface IMedicineRepository : IRepository<Medicine>
    {
        void Update (Medicine medicine);
    }
}