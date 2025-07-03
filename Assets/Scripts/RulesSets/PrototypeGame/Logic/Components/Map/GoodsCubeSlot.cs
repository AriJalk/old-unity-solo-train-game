using CommonEngine.Interfaces;
using System;

namespace PrototypeGame.Logic
{
	public class GoodsCubeSlot : IIdentifiable
	{
		public Guid guid { get; set; }

		public GoodsCube GoodsCube { get; set; }

		public GoodsCubeSlot(Guid guid)
		{
			this.guid = guid;
		}
	}
}
