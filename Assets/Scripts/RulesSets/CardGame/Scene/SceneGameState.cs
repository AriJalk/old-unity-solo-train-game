using HexSystem;
using System;
using System.Collections.Generic;

namespace CardGame.Scene
{
	internal class SceneGameState
	{
		public readonly Dictionary<HexCoord, HexTileObject> Tiles;
		public readonly Dictionary<Guid, GoodsCubeSlotObject> CubeSlots;
		public readonly Dictionary<Guid, GoodsCubeObject> Cubes;

		public SceneGameState()
		{
			Tiles = new Dictionary<HexCoord, HexTileObject>();
			CubeSlots = new Dictionary<Guid, GoodsCubeSlotObject>();
			Cubes = new Dictionary<Guid, GoodsCubeObject>();
		}
	}
}
