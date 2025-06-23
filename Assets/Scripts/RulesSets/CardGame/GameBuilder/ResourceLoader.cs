using CommonEngine.Core;
using GameEngine.Map;

namespace CardGame.GameBuilder
{
	internal class ResourceLoader
	{
		public static void LoadResources(CommonServices _serviceLocator)
		{
			_serviceLocator.PrefabManager.LoadAndRegisterPrefab<HexTileBaseObject>("CardGame/HexTilePrefab");
		}
	}
}
