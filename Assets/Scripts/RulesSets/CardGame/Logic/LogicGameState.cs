using HexSystem;
using System;
using System.Collections.Generic;

namespace CardGame.Logic
{
	internal class LogicGameState
	{
		private Dictionary<HexCoord, HexTileData> _tiles;

		public LogicGameState() { 
			_tiles = new Dictionary<HexCoord, HexTileData>();

		}
	}
}
