using HexSystem;
using PrototypeGame.Events.CommandRequestEvents;
using PrototypeGame.Logic;
using PrototypeGame.Logic.MetaData;
using PrototypeGame.Logic.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeGame.Events.CommandEventHandlers
{
	internal class MapCommandEventsHandler : IDisposable
	{
		private LogicMapStateManager _logicMapStateManager;
		private SceneMapEvents _sceneMapEvents;
		private MapCommandRequestEvents _mapCommandRequestEvents;

		public MapCommandEventsHandler(LogicMapStateManager logicMapStateManager, SceneMapEvents sceneMapEvents, MapCommandRequestEvents mapCommandRequestEvents)
		{
			_logicMapStateManager = logicMapStateManager;
			_sceneMapEvents = sceneMapEvents;
			_mapCommandRequestEvents = mapCommandRequestEvents;

			_mapCommandRequestEvents.BuildFactoryRequestEvent += OnBuildFactoryRequest;
			_mapCommandRequestEvents.BuildStationRequestEvent += OnBuildStationRequest;
			_mapCommandRequestEvents.ProduceGoodsCubeInSlotRequestEvent += OnProduceGoodsCubeOnSlotRequest;
			_mapCommandRequestEvents.RemoveFactoryRequestEvent += OnRemoveFactoryRequest;
			_mapCommandRequestEvents.RemoveGoodsCubeFromSlotRequestEvent += OnRemoveGoodsCubeFromSlotRequest;
			_mapCommandRequestEvents.RemoveStationRequestEvent += OnRemoveStationRequest;
			_mapCommandRequestEvents.TransportRequestEvent += OnTransportRequest;
		}

		public void Dispose()
		{
			_mapCommandRequestEvents.BuildFactoryRequestEvent -= OnBuildFactoryRequest;
			_mapCommandRequestEvents.BuildStationRequestEvent -= OnBuildStationRequest;
			_mapCommandRequestEvents.ProduceGoodsCubeInSlotRequestEvent -= OnProduceGoodsCubeOnSlotRequest;
			_mapCommandRequestEvents.RemoveFactoryRequestEvent -= OnRemoveFactoryRequest;
			_mapCommandRequestEvents.RemoveGoodsCubeFromSlotRequestEvent -= OnRemoveGoodsCubeFromSlotRequest;
			_mapCommandRequestEvents.RemoveStationRequestEvent -= OnRemoveStationRequest;
			_mapCommandRequestEvents.TransportRequestEvent -= OnTransportRequest;
		}

		private void OnTransportRequest(Guid origin, Guid destination)
		{
			SlotInfo originSlotInfo = _logicMapStateManager.LogicMapState.CubeSlotInfo[origin];
			SlotInfo destinationSlotInfo = _logicMapStateManager.LogicMapState.CubeSlotInfo[destination];
			_logicMapStateManager.TransportGoodsCube(originSlotInfo.Slot, destinationSlotInfo.Slot);
			_sceneMapEvents.RaiseTransportCubeEvent(origin, destination);
		}

		private void OnBuildFactoryRequest(HexCoord hexCoord, GoodsColor productionColor)
		{
			HexTileData hexTileData = _logicMapStateManager.LogicMapState.Tiles[hexCoord];

			if (hexTileData.Factory == null)
			{
				Factory factory = _logicMapStateManager.BuildFactoryOnTile(hexTileData, productionColor);
				//_logicMapStateManager.ProduceGoodsCubeInSlot(factory.GoodsCubeSlot, factory.ProductionColor);

				_sceneMapEvents.RaiseFactoryBuiltEvent(hexTileData);
			}
		}

		private void OnRemoveFactoryRequest(HexCoord hexCoord)
		{
			HexTileData hexTileData = _logicMapStateManager.LogicMapState.Tiles[hexCoord];

			if (hexTileData.Factory != null)
			{
				_logicMapStateManager.RemoveFactory(hexTileData);
				_sceneMapEvents.RaiseFactoryRemovedEvent(hexTileData);
			}
		}

		private void OnBuildStationRequest(HexCoord hexCoord)
		{
			HexTileData hexTileData = _logicMapStateManager.LogicMapState.Tiles[hexCoord];

			if (hexTileData.Station == null)
			{
				_logicMapStateManager.BuildStationOnTile(hexTileData);
				_sceneMapEvents.RaiseStationBuiltEvent(hexTileData);
			}
		}

		private void OnRemoveStationRequest(HexCoord hexCoord)
		{
			HexTileData hexTileData = _logicMapStateManager.LogicMapState.Tiles[hexCoord];

			if (hexTileData.Station != null)
			{
				_logicMapStateManager.RemoveStation(hexTileData);
				_sceneMapEvents.RaiseStationRemovedEvent(hexTileData);
			}
		}

		private void OnProduceGoodsCubeOnSlotRequest(Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			GoodsCubeSlot goodsCubeSlot = _logicMapStateManager.LogicMapState.CubeSlotInfo[goodsCubeSlotGuid].Slot;
			GoodsCube cube = _logicMapStateManager.ProduceGoodsCubeInSlot(goodsCubeSlot, goodsColor);
			_sceneMapEvents.RaiseGoodsCubeProducedInSlotEvent(goodsCubeSlot, cube);
		}

		private void OnRemoveGoodsCubeFromSlotRequest(Guid goodsCubeSlotGuid)
		{
			GoodsCubeSlot goodsCubeSlot = _logicMapStateManager.LogicMapState.CubeSlotInfo[goodsCubeSlotGuid].Slot;
			_logicMapStateManager.RemoveCube(goodsCubeSlot.GoodsCube);
			_sceneMapEvents.RaiseGoodsCubeRemovedFromSlotEvent(goodsCubeSlot);
		}


	}
}
