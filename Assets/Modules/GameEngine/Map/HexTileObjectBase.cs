using HexSystem;

namespace GameEngine.Map
{
	/// <summary>
	/// Base class for HexTiles in the scene tree
	/// </summary>
	public class HexTileObjectBase : MeshComponent
	{
		public HexCoord HexCoord { get; set; }
	}
}