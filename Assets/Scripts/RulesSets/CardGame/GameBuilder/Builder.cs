using CardGame.Logic;
using CardGame.Logic.Services;
using CardGame.Services;
using HexSystem;

namespace CardGame.GameBuilder
{
	internal class Builder
	{
		public static void Build(GameStateServices gameStateServices, LogicStateManager logicManager)
		{
			HexCoord coord = HexCoord.GetCoord(0, 0);
			HexTileData tile = logicManager.BuildTile(coord, TerrainType.FIELD);
			logicManager.BuildStationOnTile(tile);

			gameStateServices.GameStateEvents.RaiseTileBuiltEvent(tile);

			foreach (HexCoord neighbor in coord.GetNeighbors())
			{
				tile = logicManager.BuildTile(neighbor, TerrainType.MOUNTAIN);
				Factory factory = logicManager.BuildFactoryOnTile(tile, GoodsColor.GREEN);
				logicManager.ProduceCubeInFactory(factory);

				gameStateServices.GameStateEvents.RaiseTileBuiltEvent(tile);
			}
		}
	}
}
