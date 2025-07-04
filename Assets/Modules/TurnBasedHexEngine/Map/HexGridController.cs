using HexSystem;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedHexEngine.Map
{
	/// <summary>
	/// Manages hexagonal tiles in the scene
	/// </summary>
	public class HexGridController : MonoBehaviour
	{
		private Dictionary<HexCoord, HexTileObjectBase> _hexDictionary = new Dictionary<HexCoord, HexTileObjectBase>();
		// Start is called once before the first execution of Update after the MonoBehaviour is created

		private HexLayout _layout = new HexLayout(HexOrientation.FlatLayout, 0.5f, 0.025f);

		public void AddTileToGrid(HexTileObjectBase tile)
		{
			tile.transform.SetParent(transform);
			tile.transform.position = HexCoord.CoordToWorld(tile.HexCoord, _layout);
		}

		public HexTileObjectBase RemoveTileFromGrid(HexCoord coord)
		{
			if (_hexDictionary.ContainsKey(coord))
			{
				HexTileObjectBase tile = _hexDictionary[coord];
				_hexDictionary.Remove(coord);
				return tile;
			}
			return null;
		}

		public HexTileObjectBase GetTileFromGrid(HexCoord coord)
		{
			if (_hexDictionary.ContainsKey(coord))
			{
				return _hexDictionary[coord];
			}
			return null;
		}
	}
}
