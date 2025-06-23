using HexSystem;
using System;
using System.Collections.Generic;

namespace CardGame.Logic
{
	internal class LogicGameState
	{
		private Dictionary<HexCoord, HexTileData> _tiles;
		private Dictionary<Guid, GoodsCubeSlot> _cubeSlots;
		private Dictionary<HexCoord, List<Guid>> _slotsOnTile;

		public LogicGameState() { 
			_tiles = new Dictionary<HexCoord, HexTileData>();
			_cubeSlots = new Dictionary<Guid, GoodsCubeSlot>();
		}
	}
}
