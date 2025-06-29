using CardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeGame.Scene.State.Cards
{
	internal class SceneCardState
	{
		public readonly Dictionary<Guid, CardObjectBase> Cards;

		public SceneCardState()
		{
			Cards = new Dictionary<Guid, CardObjectBase>();
		}
	}
}
