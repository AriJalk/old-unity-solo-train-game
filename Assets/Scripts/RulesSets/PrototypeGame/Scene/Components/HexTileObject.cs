using GameEngine.Map;
using UnityEngine;

namespace PrototypeGame.Scene
{
	public class HexTileObject : HexTileObjectBase
	{
		public Transform FactoryContainer;
		public Transform StationContainer;

		public FactoryObject FactoryObject {  get; set; }
		public StationObject StationObject { get; set; }
	}

}
