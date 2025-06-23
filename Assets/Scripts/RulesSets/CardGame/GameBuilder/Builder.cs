using CardGame.Logic;
using CardGame.Scene;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
using HexSystem;


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

			HexTileObject tileObject = manipulator.BuildSceneTile(tileData);

			gameServices.HexGridController.AddTileToGrid(tileObject);

			foreach (HexCoord neighborCoord in coord.GetNeighbors())
			{
				gameServices.HexGridController.AddTileToGrid(manipulator.BuildSceneTile(new HexTileData(neighborCoord)));
			}
		}
	}
}
