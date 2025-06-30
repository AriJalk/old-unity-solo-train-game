using CommonEngine.Core;
using HexSystem;

namespace TurnBasedHexEngine.Map
{
	/// <summary>
	/// Base class for HexTiles in the scene tree
	/// </summary>
	public class HexTileObjectBase : MeshComponent
	{
		public HexCoord HexCoord { get; set; }
	}
}