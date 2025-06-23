using HexSystem;

namespace CardGame.Logic
{
	internal class HexTileData
	{
		public HexCoord HexCoord;
		public Factory Factory;
		public Station Station;

		public HexTileData(HexCoord hexCoord)
		{ 
			HexCoord = hexCoord;
		}
	}
}
