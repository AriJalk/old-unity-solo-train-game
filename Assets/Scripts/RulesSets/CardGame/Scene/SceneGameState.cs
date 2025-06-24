
using CardGame.Logic;
using HexSystem;
using System;
using System.Collections.Generic;

namespace CardGame.Scene
{
	internal class SceneGameState
	{
		private Dictionary<HexCoord, HexTileObject> _tiles;
		private Dictionary<Guid, GoodsCubeSlotObject> _cubeSlots;
		//private Dictionary<HexCoord, List<Guid>> _slotsOnTile;

		public SceneGameState()
		{
			_tiles = new Dictionary<HexCoord, HexTileObject>();
			_cubeSlots = new Dictionary<Guid, GoodsCubeSlotObject>();
			//_slotsOnTile = new Dictionary<HexCoord, List<Guid>>();
		}
	}
}
