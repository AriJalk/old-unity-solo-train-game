using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeGame.Logic.ServiceContracts
{
	internal interface IFactoryLookupService
	{
		IEnumerable<Factory> GetEmptyFactories();
	}
}
