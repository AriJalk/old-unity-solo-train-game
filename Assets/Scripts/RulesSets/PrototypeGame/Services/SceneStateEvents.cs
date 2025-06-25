
using PrototypeGame.Logic;
using PrototypeGame.Scene;
using System;

namespace PrototypeGame
{
	internal class SceneStateEvents
	{
		public event Action<HexTileData> TileBuiltEvent;

		public event Action<HexTileData> FactoryBuiltEvent;

		public event Action<Guid, Guid> TransportCubeEvent;

		public void RaiseTileBuiltEvent(HexTileData data)
		{
			TileBuiltEvent?.Invoke(data);
		}

		public void RaiseTransportCubeEvent(Guid originSlot, Guid destinationSlot)
		{
			TransportCubeEvent?.Invoke(originSlot, destinationSlot);
		}

		public void RaiseFactoryBuiltEvent(HexTileData hexTileData)
		{
			FactoryBuiltEvent?.Invoke(hexTileData);
		}
	}

}
