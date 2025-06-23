using CardGame.Scene;
using CommonEngine.Core;

namespace CardGame.GameBuilder
{
	internal class ResourceLoader
	{
		public static void LoadResources(CommonServices _serviceLocator)
		{
			_serviceLocator.PrefabManager.LoadAndRegisterPrefab<HexTileObject>("CardGame/HexTilePrefab");
			_serviceLocator.PrefabManager.LoadAndRegisterPrefab<FactoryObject>("CardGame/FactoryPrefab");
			_serviceLocator.PrefabManager.LoadAndRegisterPrefab<GoodsCubeObject>("CardGame/GoodsCubePrefab");
		}
	}
}
