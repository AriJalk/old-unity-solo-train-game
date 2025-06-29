
using PrototypeGame.Logic;
using System;

namespace PrototypeGame
{
	/// <summary>
	/// Main API connection point to the Scene, all possible scene interactions goes through here
	/// Only accessed through CommandEventHandler
	/// </summary>
	internal class SceneMapEvents
	{
		public event Action<HexTileData> TileBuiltEvent;

		public event Action<HexTileData> FactoryBuiltEvent;
		public event Action<HexTileData> FactoryRemovedEvent;

		public event Action<HexTileData> StationBuiltEvent;
		public event Action<HexTileData> StationRemovedEvent;

		public event Action<GoodsCube, Guid> GoodsCubeProducedInSlotEvent;
		public event Action<Guid> GoodsCubeRemovedFromSlotEvent;

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

		public void RaiseFactoryRemovedEvent(HexTileData hexTileData)
		{
			FactoryRemovedEvent?.Invoke(hexTileData);
		}

		public void RaiseStationBuiltEvent(HexTileData hexTileData)
		{
			StationBuiltEvent?.Invoke(hexTileData);
		}

		public void RaiseStationRemovedEvent(HexTileData hexTileData)
		{
			StationRemovedEvent?.Invoke(hexTileData);
		}

		public void RaiseGoodsCubeProducedInSlotEvent(GoodsCubeSlot goodsCubeSlot, GoodsCube goodsCube)
		{
			GoodsCubeProducedInSlotEvent?.Invoke(goodsCube, goodsCubeSlot.guid);
		}

		public void RaiseGoodsCubeRemovedFromSlotEvent(GoodsCubeSlot goodsCubeSlot)
		{
			GoodsCubeRemovedFromSlotEvent?.Invoke(goodsCubeSlot.guid);
		}
	}

}
