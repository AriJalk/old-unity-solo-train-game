using HexSystem;
using System;

namespace PrototypeGame
{
	internal class LogicStateEvents
	{
		public event Action<Guid, Guid> TransportRequestEvent;

		public event Action<HexCoord, GoodsColor> BuildFactoryRequestEvent;
		public event Action<HexCoord> RemoveFactoryRequestEvent;

		public event Action<HexCoord> BuildStationRequestEvent;
		public event Action<HexCoord> RemoveStationRequestEvent;

		public void RaiseTransportRequestEvent(Guid originSlot, Guid destinationSlot)
		{
			TransportRequestEvent?.Invoke(originSlot, destinationSlot);
		}

		public void RaiseBuildFactoryRequestEvent(HexCoord hexCoord, GoodsColor productionColor)
		{
			BuildFactoryRequestEvent?.Invoke(hexCoord, productionColor);
		}

		public void RaiseRemoveFactoryRequestEvent(HexCoord hexCoord)
		{
			RemoveFactoryRequestEvent?.Invoke(hexCoord);
		}

		public void RaiseBuildStationRequestEvent(HexCoord hexCoord)
		{
			BuildStationRequestEvent?.Invoke(hexCoord);
		}

		public void RaiseRemoveStationRequestEvent(HexCoord hexCoord)
		{
			RemoveStationRequestEvent?.Invoke(hexCoord);
		}
	}
}
