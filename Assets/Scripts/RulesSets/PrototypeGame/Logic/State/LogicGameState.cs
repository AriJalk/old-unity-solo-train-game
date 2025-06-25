using PrototypeGame.Logic.MetaData;
using HexSystem;
using System;
using System.Collections.Generic;

namespace PrototypeGame.Logic.State
{
	/// <summary>
	/// Modify only from LogicStateManager, can be read by other classes
	/// </summary>
	internal class LogicGameState
	{
		public readonly Dictionary<HexCoord, HexTileData> Tiles;
		public readonly Dictionary<Guid, SlotInfo> CubeSlotInfo;
		public readonly Dictionary<Guid, GoodsCubeSlot> CubeToSlot;
		public readonly Dictionary<HexTileData, ICollection<GoodsCubeSlot>> TileToSlots;
		public readonly Dictionary<Guid, Factory> Factories;
		public readonly Dictionary<Guid, Station> Stations;

		public LogicGameState() { 
			Tiles = new Dictionary<HexCoord, HexTileData>();
			CubeSlotInfo = new Dictionary<Guid, SlotInfo>();
			CubeToSlot = new Dictionary<Guid, GoodsCubeSlot>();
			TileToSlots = new Dictionary<HexTileData, ICollection<GoodsCubeSlot>>();
			Factories = new Dictionary<Guid, Factory>();
			Stations = new Dictionary<Guid, Station>();
		}
	}
}
