using PrototypeGame.Logic;
using PrototypeGame.Logic.Services;
using PrototypeGame.Services;
using HexSystem;

namespace PrototypeGame.GameBuilder
{
	internal class Builder
	{
		public static void Build(GameStateEvents gameStateServices, LogicStateManager logicManager)
		{
			HexCoord coord = HexCoord.GetCoord(0, 0);
			HexTileData tile = logicManager.BuildTile(coord, TerrainType.MOUNTAIN);

			gameStateServices.SceneStateEvents.RaiseTileBuiltEvent(tile);

			foreach (HexCoord neighbor in coord.GetNeighbors())
			{
				tile = logicManager.BuildTile(neighbor, TerrainType.FIELD);

				gameStateServices.SceneStateEvents.RaiseTileBuiltEvent(tile);
			}
		}
	}
}
