using System.Collections.Generic;

namespace PrototypeGame.Logic.ServiceContracts
{
	internal interface IFactoryLookupService
	{
		IEnumerable<Factory> GetEmptyFactories();
	}
}
