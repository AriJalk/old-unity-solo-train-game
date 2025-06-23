using HexSystem;
using System;
using Unity.VisualScripting;

namespace CardGame.Logic
{
	public class GoodsCubeSlot : IIdentifiable
	{
		public Guid guid { get; set; }

		public GoodsCube GoodsCube { get; set; }

		public HexCoord HexCoord { get; set; }

		public GoodsCubeSlot(Guid guid, HexCoord hexCoord)
		{
			this.guid = guid;
			HexCoord = hexCoord;
		}
	}
}
