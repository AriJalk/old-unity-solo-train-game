using HexSystem;
using PrototypeGame.Logic;
using System;

namespace PrototypeGame
{
	/// <summary>
	/// Main API for Commands to interact with the game state
	/// </summary>
	internal class CommandRequestEvents
	{
		public event Action<Guid, Guid> TransportRequestEvent;

		public event Action<HexCoord, GoodsColor> BuildFactoryRequestEvent;
		public event Action<HexCoord> RemoveFactoryRequestEvent;

		public event Action<HexCoord> BuildStationRequestEvent;
		public event Action<HexCoord> RemoveStationRequestEvent;

		public event Action<Guid, GoodsColor> ProduceGoodsCubeInSlotRequestEvents;
		public event Action<Guid> RemoveGoodsCubeFromSlotRequestEvents;

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

		public void RaiseProduceGoodsCubeInSlotRequestEvents(Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			ProduceGoodsCubeInSlotRequestEvents?.Invoke(goodsCubeSlotGuid, goodsColor);
		}

		public void RaiseRemoveGoodsCubeFromSlotRequestEvents(Guid goodsCubeSlotGuid)
		{
			RemoveGoodsCubeFromSlotRequestEvents?.Invoke(goodsCubeSlotGuid);
		}
	}
}
