using HexGridSystem;
using System.Collections.Generic;
using UnityEngine;

public class HexGridController : MonoBehaviour
{
	private Dictionary<HexCoord, HexTileObject> _hexDictionary = new Dictionary<HexCoord, HexTileObject>();
	// Start is called once before the first execution of Update after the MonoBehaviour is created

	private HexLayout _layout = new HexLayout(HexOrientation.FlatLayout, 0.5f, 0.1f);


	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void AddTileToGrid(HexTileObject tile)
	{
		tile.transform.SetParent(transform);
		tile.transform.position = HexCoord.CoordToWorld(tile.HexCoord, _layout);
	}

	public HexTileObject RemoveTileFromGrid(HexCoord coord)
	{
		if (_hexDictionary.ContainsKey(coord))
		{
			HexTileObject tile = _hexDictionary[coord];
			_hexDictionary.Remove(coord);
			return tile;
		}

		return null;
	}
}
