using HexSystem;
using System;

namespace CardGame.Logic.Services
{
	internal static class LogicStateManipulator
	{
		public static HexTileData BuildTile(HexCoord coordinates, TerrainType terrainType)
		{
			HexTileData hexTileData = new HexTileData(coordinates, terrainType);

			return hexTileData;
		}

		public static HexTileData BuildFactoryOnTile(HexTileData hexTileData, GoodsColor productionColor)
		{
			Factory factory = new Factory(productionColor);
			factory.GoodsCubeSlot = new GoodsCubeSlot(Guid.NewGuid());
			hexTileData.Factory = factory;
			return hexTileData;
		}

		public static GoodsCube ProduceCubeInFactory(Factory factory)
		{
			GoodsCube cube = new GoodsCube(Guid.NewGuid(), factory.ProductionColor);
			factory.GoodsCubeSlot.GoodsCube = cube;
			return cube;
		}

		public static void TransportGoodsCube(GoodsCubeSlot origin, GoodsCubeSlot destination)
		{
			destination.GoodsCube = origin.GoodsCube;
			origin.GoodsCube = null;
		}
	}
}
