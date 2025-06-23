using HexSystem;
using System;
using Unity.VisualScripting;

namespace CardGame.Logic
{
	public class GoodsCubeSlot : IIdentifiable
	{
		public Guid guid { get; set; }

		public Guid CubeId { get; set; }

		public HexCoord HexCoord { get; set; }
	}
}
