using CardGame.Logic;
using CommonEngine.Core;
using CommonEngine.ResourceManagement;
using GameEngine.Map;

namespace CardGame.Scene
{
	internal class TileManipulator
	{
		private PrefabManager _prefabManager;
		public TileManipulator(CommonServices commonServices)
		{
			_prefabManager = commonServices.PrefabManager;
		}

		public HexTileBaseObject BuildSceneTile(TerrainTileData data)
		{
			HexTileBaseObject tile = _prefabManager.RetrievePoolObject<HexTileBaseObject>();
			if (tile == null)
			{
				return null;
			}
			tile.HexCoord = data.HexCoord;

			return tile;
		}
	}
}