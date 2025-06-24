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
		public static void Build(CommonServices commonServices, GameEngineServices gameServices, GameStateServices gameStateServices)
		{
			ResourceLoader.LoadResources(commonServices);

			HexCoord coord = HexCoord.GetCoord(0, 0);
			HexTileData tile = LogicStateManipulator.BuildTile(coord, TerrainType.FIELD);
			LogicStateManipulator.BuildFactoryOnTile(tile, GoodsColor.GREEN);
			gameStateServices.GameStateEvents.RaiseTileBuiltEvent(tile);

			foreach (HexCoord neighbor in coord.GetNeighbors())
			{
				tile = LogicStateManipulator.BuildTile(neighbor, TerrainType.MOUNTAIN);
				gameStateServices.GameStateEvents.RaiseTileBuiltEvent(tile);
			}
		}
	}
}
