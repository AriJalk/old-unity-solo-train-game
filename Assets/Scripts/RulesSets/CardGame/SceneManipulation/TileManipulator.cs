using CardGame.Logic;
using CommonEngine.Core;
using CommonEngine.ResourceManagement;
using UnityEngine;

namespace CardGame.Scene
{
	public class TileManipulator
	{
		private PrefabManager _prefabManager;
		public TileManipulator(CommonServiceLocator serviceLocator)
		{
			_prefabManager = serviceLocator.PrefabManager;
		}

		public HexTileObject BuildSceneTile(HexTileData data)
		{
			HexTileObject tile = _prefabManager.RetrievePoolObject<HexTileObject>();
			if (tile == null)
			{
				return null;
			}
			tile.HexCoord = data.HexCoord;

			return tile;
		}
	}
}