using CardGame.Logic;
using CardGame.Logic.Services;
using CardGame.Services;
using CommonEngine.Core;
using GameEngine.Core;
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
				logicManager.BuildFactoryOnTile(tile, GoodsColor.GREEN);

				gameStateServices.GameStateEvents.RaiseTileBuiltEvent(tile);
			}
		}
	}
}
