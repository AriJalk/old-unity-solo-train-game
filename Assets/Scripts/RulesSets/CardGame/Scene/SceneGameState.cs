
using System;
using System.Collections.Generic;

namespace CardGame.Scene
{
	internal class SceneGameState
	{
		private Dictionary<Guid, GoodsCubeSlotObject> _goodsCubeSlotObjects;

		public SceneGameState()
		{
			_goodsCubeSlotObjects = new Dictionary<Guid, GoodsCubeSlotObject>();
		}
	}
}
