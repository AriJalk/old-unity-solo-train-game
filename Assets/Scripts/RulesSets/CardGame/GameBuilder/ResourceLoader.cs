using CardGame.Scene;
using CommonEngine.Core;

namespace CardGame.GameBuilder
{
	internal class ResourceLoader
	{
		public static void LoadResources(CommonServices _commonServices)
		{
			_commonServices.PrefabManager.LoadAndRegisterPrefab<HexTileObject>("CardGame/HexTilePrefab");
			_commonServices.PrefabManager.LoadAndRegisterPrefab<FactoryObject>("CardGame/FactoryPrefab");
			_commonServices.PrefabManager.LoadAndRegisterPrefab<GoodsCubeObject>("CardGame/GoodsCubePrefab");
			_commonServices.PrefabManager.LoadAndRegisterPrefab<StationObject>("CardGame/StationPrefab");
		}
	}
}
