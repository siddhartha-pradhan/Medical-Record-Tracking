using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Infrastructure.Persistence.Seed
{
	public interface IDbInitializer
	{
		Task Initialize();
	}
}
