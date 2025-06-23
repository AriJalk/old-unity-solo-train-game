using HexSystem;
using UnityEngine;

namespace GameEngine.Map
{
	/// <summary>
	/// Base class for HexTiles in the scene tree
	/// </summary>
	public class HexTileObject : MonoBehaviour
	{
		public HexCoord HexCoord { get; set; }
	}
}