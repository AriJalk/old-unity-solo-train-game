using PrototypeGame.Logic;
using PrototypeGame.Logic.Services;
using PrototypeGame.Services;
using HexSystem;

namespace PrototypeGame.GameBuilder
{
	internal class Builder
	{
		public static void Build(GameStateServices gameStateServices, LogicStateManager logicManager)
		{
			HexCoord coord = HexCoord.GetCoord(0, 0);
			HexTileData tile = logicManager.BuildTile(coord, TerrainType.MOUNTAIN);
			logicManager.BuildStationOnTile(tile);

			gameStateServices.GameStateEvents.RaiseTileBuiltEvent(tile);

			foreach (HexCoord neighbor in coord.GetNeighbors())
			{
				tile = logicManager.BuildTile(neighbor, TerrainType.FIELD);
				Factory factory = logicManager.BuildFactoryOnTile(tile, GoodsColor.BLUE);
				logicManager.ProduceCubeInFactory(factory);

				gameStateServices.GameStateEvents.RaiseTileBuiltEvent(tile);
			}
		}
	}
}
