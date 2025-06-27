using HexSystem;
using PrototypeGame.Logic;
using PrototypeGame.Logic.MetaData;
using PrototypeGame.Logic.State;
using System;

namespace PrototypeGame.Events
{
	/// <summary>
	/// Orchestrating logic + scene command execution
	/// </summary>
	internal class CommandEventHandler : IDisposable
	{
		private LogicStateManager _logicStateManager;

		private CommandRequestEvents _commandRequestEvents;
		private SceneStateEvents _sceneStateEvents;

		public CommandEventHandler(LogicStateManager logicStateManager, GameStateEvents gameStateServices)
		{
			_logicStateManager = logicStateManager;
			_commandRequestEvents = gameStateServices.CommandRequestEvents;
			_sceneStateEvents = gameStateServices.SceneStateEvents;

			_commandRequestEvents.TransportRequestEvent += TransportRequest;

			_commandRequestEvents.BuildFactoryRequestEvent += BuildFactoryRequest;
			_commandRequestEvents.RemoveFactoryRequestEvent += RemoveFactoryRequest;

			_commandRequestEvents.BuildStationRequestEvent += BuildStationRequest;
			_commandRequestEvents.RemoveStationRequestEvent += RemoveStationRequest;

			_commandRequestEvents.ProduceGoodsCubeInSlotRequestEvents += ProduceGoodsCubeOnSlotRequest;
			_commandRequestEvents.RemoveGoodsCubeFromSlotRequestEvents += RemoveGoodsCubeFromSlotRequest;
		}

		public void Dispose()
		{
			_commandRequestEvents.TransportRequestEvent -= TransportRequest;

			_commandRequestEvents.BuildFactoryRequestEvent -= BuildFactoryRequest;
			_commandRequestEvents.RemoveFactoryRequestEvent -= RemoveFactoryRequest;

			_commandRequestEvents.BuildStationRequestEvent -= BuildStationRequest;
			_commandRequestEvents.RemoveStationRequestEvent -= RemoveStationRequest;

			_commandRequestEvents.ProduceGoodsCubeInSlotRequestEvents -= ProduceGoodsCubeOnSlotRequest;
			_commandRequestEvents.RemoveGoodsCubeFromSlotRequestEvents -= RemoveGoodsCubeFromSlotRequest;
		}

		private void TransportRequest(Guid origin, Guid destination)
		{
			SlotInfo originSlotInfo = _logicStateManager.LogicGameState.CubeSlotInfo[origin];
			SlotInfo destinationSlotInfo = _logicStateManager.LogicGameState.CubeSlotInfo[destination];
			_logicStateManager.TransportGoodsCube(originSlotInfo.Slot, destinationSlotInfo.Slot);
			_sceneStateEvents.RaiseTransportCubeEvent(origin, destination);
		}

		private void BuildFactoryRequest(HexCoord hexCoord, GoodsColor productionColor)
		{
			HexTileData hexTileData = _logicStateManager.LogicGameState.Tiles[hexCoord];

			if (hexTileData.Factory == null)
			{
				Factory factory = _logicStateManager.BuildFactoryOnTile(hexTileData, productionColor);
				//_logicStateManager.ProduceGoodsCubeInSlot(factory.GoodsCubeSlot, factory.ProductionColor);

				_sceneStateEvents.RaiseFactoryBuiltEvent(hexTileData);
			}
		}

		private void RemoveFactoryRequest(HexCoord hexCoord)
		{
			HexTileData hexTileData = _logicStateManager.LogicGameState.Tiles[hexCoord];

			if (hexTileData.Factory != null)
			{
				_logicStateManager.RemoveFactory(hexTileData);
				_sceneStateEvents.RaiseFactoryRemoveEvent(hexTileData);
			}
		}

		private void BuildStationRequest(HexCoord hexCoord)
		{
			HexTileData hexTileData = _logicStateManager.LogicGameState.Tiles[hexCoord];

			if (hexTileData.Station == null)
			{
				_logicStateManager.BuildStationOnTile(hexTileData);
				_sceneStateEvents.RaiseStationBuiltEvent(hexTileData);
			}
		}

		private void RemoveStationRequest(HexCoord hexCoord)
		{
			HexTileData hexTileData = _logicStateManager.LogicGameState.Tiles[hexCoord];

			if (hexTileData.Station != null)
			{
				_logicStateManager.RemoveStation(hexTileData);
				_sceneStateEvents.RaiseStationRemovedEvent(hexTileData);
			}
		}

		private void ProduceGoodsCubeOnSlotRequest(Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			GoodsCubeSlot goodsCubeSlot = _logicStateManager.LogicGameState.CubeSlotInfo[goodsCubeSlotGuid].Slot;
			GoodsCube cube = _logicStateManager.ProduceGoodsCubeInSlot(goodsCubeSlot, goodsColor);
			_sceneStateEvents.RaiseGoodsCubeProducedInSlotEvent(goodsCubeSlot, cube);
		}

		private void RemoveGoodsCubeFromSlotRequest(Guid goodsCubeSlotGuid)
		{
			GoodsCubeSlot goodsCubeSlot = _logicStateManager.LogicGameState.CubeSlotInfo[goodsCubeSlotGuid].Slot;
			_logicStateManager.RemoveCube(goodsCubeSlot.GoodsCube);
			_sceneStateEvents.RaiseGoodsCubeRemovedFromSlotEvent(goodsCubeSlot);
		}
	}
}
