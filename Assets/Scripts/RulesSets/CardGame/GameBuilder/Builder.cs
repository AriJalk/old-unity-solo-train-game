using CardGame.Logic;
using CardGame.Scene;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
using HexSystem;
using System;


namespace CardGame.GameBuilder
{
	internal class Builder
	{
		public static void Build(CommonServices commonServices, GameServices gameServices)
		{
			ResourceLoader.LoadResources(commonServices);

			HexCoord coord = HexCoord.GetCoord(0, 0);

			TileManipulator manipulator = new TileManipulator(commonServices);

			HexTileData tileData = new HexTileData(coord);

			GameEngine.Map.HexTileObjectBase tileObject = manipulator.BuildSceneTile(tileData);

			gameServices.HexGridController.AddTileToGrid(tileObject);

			foreach (HexCoord neighborCoord in coord.GetNeighbors())
			{
				tileData = new HexTileData(neighborCoord);
				tileData.Factory = new Factory(GoodsColor.GREEN);
				tileData.Factory.GoodsCubeSlot = new GoodsCubeSlot(Guid.NewGuid(), neighborCoord);
				tileData.Factory.GoodsCubeSlot.GoodsCube = new GoodsCube(Guid.NewGuid(), tileData.Factory.ProductionColor);
				gameServices.HexGridController.AddTileToGrid(manipulator.BuildSceneTile(tileData));
			}
		}
	}
}
