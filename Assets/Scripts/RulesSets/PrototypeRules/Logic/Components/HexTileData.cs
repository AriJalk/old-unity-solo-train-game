using HexSystem;

namespace PrototypeGame.Logic
{
	internal class HexTileData
	{
		public HexCoord HexCoord;
		public TerrainType TerrainType;
		public Factory Factory;
		public Station Station;

		public HexTileData(HexCoord hexCoord, TerrainType terrainType)
		{ 
			HexCoord = hexCoord;
			TerrainType = terrainType;
		}
	}
}
