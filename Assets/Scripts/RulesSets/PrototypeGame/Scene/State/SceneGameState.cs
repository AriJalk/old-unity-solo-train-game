using HexSystem;
using System;
using System.Collections.Generic;

namespace PrototypeGame.Scene.State
{
	/// <summary>
	/// Contains all references to scene game objects
	/// </summary>
	internal class SceneGameState
	{
		public readonly Dictionary<HexCoord, HexTileObject> Tiles;
		public readonly Dictionary<Guid, GoodsCubeSlotObject> CubeSlots;
		public readonly Dictionary<Guid, GoodsCubeObject> Cubes;
		public readonly Dictionary<Guid, Guid> CubeToSlot;

		public SceneGameState()
		{
			Tiles = new Dictionary<HexCoord, HexTileObject>();
			CubeSlots = new Dictionary<Guid, GoodsCubeSlotObject>();
			Cubes = new Dictionary<Guid, GoodsCubeObject>();
			CubeToSlot = new Dictionary<Guid, Guid>();
		}
	}
}
