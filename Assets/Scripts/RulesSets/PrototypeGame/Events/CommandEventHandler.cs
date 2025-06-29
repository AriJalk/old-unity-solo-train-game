using GameEngine.StateMachine;
using HexSystem;
using PrototypeGame.Logic;
using PrototypeGame.Logic.MetaData;
using PrototypeGame.Logic.State;
using PrototypeGame.Logic.State.Cards;
using System;
using System.Diagnostics;

namespace PrototypeGame.Events
{
	/// <summary>
	/// Orchestrating logic + scene command execution
	/// </summary>
	internal class CommandEventHandler : IDisposable
	{
		private LogicMapStateManager _logicMapStateManager;
		private LogicCardStateManager _logicCardStateManager;

		private CommandRequestEvents _commandRequestEvents;
		private SceneMapEvents _sceneStateEvents;

		private StateMachineManager _stateMachineManager;

		public CommandEventHandler(LogicMapStateManager logicMapStateManager, GameStateEvents gameStateEvents, StateMachineManager stateMachineManager, LogicCardStateManager logicCardStateManager)
		{
			_logicMapStateManager = logicMapStateManager;
			_logicCardStateManager = logicCardStateManager;
			_commandRequestEvents = gameStateEvents.CommandRequestEvents;
			_sceneStateEvents = gameStateEvents.SceneMapEvents;

			_commandRequestEvents.TransportRequestEvent += TransportRequest;

			_commandRequestEvents.BuildFactoryRequestEvent += BuildFactoryRequest;
			_commandRequestEvents.RemoveFactoryRequestEvent += RemoveFactoryRequest;

			_commandRequestEvents.BuildStationRequestEvent += BuildStationRequest;
			_commandRequestEvents.RemoveStationRequestEvent += RemoveStationRequest;

			_commandRequestEvents.ProduceGoodsCubeInSlotRequestEvent += ProduceGoodsCubeOnSlotRequest;
			_commandRequestEvents.RemoveGoodsCubeFromSlotRequestEvent += RemoveGoodsCubeFromSlotRequest;

			_commandRequestEvents.TransitionToStateMachineRequestEvent += TransitionToStateMachineRequest;

			_commandRequestEvents.PlayCardActionRequestEvent += PlayCardRequest;

			_commandRequestEvents.TransitionToPreviousStateMachineRequestEvent += PreviousStateRequest;

			_stateMachineManager = stateMachineManager;
		}



		public void Dispose()
		{
			_commandRequestEvents.TransportRequestEvent -= TransportRequest;

			_commandRequestEvents.BuildFactoryRequestEvent -= BuildFactoryRequest;
			_commandRequestEvents.RemoveFactoryRequestEvent -= RemoveFactoryRequest;

			_commandRequestEvents.BuildStationRequestEvent -= BuildStationRequest;
			_commandRequestEvents.RemoveStationRequestEvent -= RemoveStationRequest;

			_commandRequestEvents.ProduceGoodsCubeInSlotRequestEvent -= ProduceGoodsCubeOnSlotRequest;
			_commandRequestEvents.RemoveGoodsCubeFromSlotRequestEvent -= RemoveGoodsCubeFromSlotRequest;

			_commandRequestEvents.TransitionToStateMachineRequestEvent -= TransitionToStateMachineRequest;

			_commandRequestEvents.PlayCardActionRequestEvent -= PlayCardRequest;
			_commandRequestEvents.TransitionToPreviousStateMachineRequestEvent -= PreviousStateRequest;
		}

		private void TransportRequest(Guid origin, Guid destination)
		{
			SlotInfo originSlotInfo = _logicMapStateManager.LogicMapState.CubeSlotInfo[origin];
			SlotInfo destinationSlotInfo = _logicMapStateManager.LogicMapState.CubeSlotInfo[destination];
			_logicMapStateManager.TransportGoodsCube(originSlotInfo.Slot, destinationSlotInfo.Slot);
			_sceneStateEvents.RaiseTransportCubeEvent(origin, destination);
		}

		private void BuildFactoryRequest(HexCoord hexCoord, GoodsColor productionColor)
		{
			HexTileData hexTileData = _logicMapStateManager.LogicMapState.Tiles[hexCoord];

			if (hexTileData.Factory == null)
			{
				Factory factory = _logicMapStateManager.BuildFactoryOnTile(hexTileData, productionColor);
				//_logicMapStateManager.ProduceGoodsCubeInSlot(factory.GoodsCubeSlot, factory.ProductionColor);

				_sceneStateEvents.RaiseFactoryBuiltEvent(hexTileData);
			}
		}

		private void RemoveFactoryRequest(HexCoord hexCoord)
		{
			HexTileData hexTileData = _logicMapStateManager.LogicMapState.Tiles[hexCoord];

			if (hexTileData.Factory != null)
			{
				_logicMapStateManager.RemoveFactory(hexTileData);
				_sceneStateEvents.RaiseFactoryRemovedEvent(hexTileData);
			}
		}

		private void BuildStationRequest(HexCoord hexCoord)
		{
			HexTileData hexTileData = _logicMapStateManager.LogicMapState.Tiles[hexCoord];

			if (hexTileData.Station == null)
			{
				_logicMapStateManager.BuildStationOnTile(hexTileData);
				_sceneStateEvents.RaiseStationBuiltEvent(hexTileData);
			}
		}

		private void RemoveStationRequest(HexCoord hexCoord)
		{
			HexTileData hexTileData = _logicMapStateManager.LogicMapState.Tiles[hexCoord];

			if (hexTileData.Station != null)
			{
				_logicMapStateManager.RemoveStation(hexTileData);
				_sceneStateEvents.RaiseStationRemovedEvent(hexTileData);
			}
		}

		private void ProduceGoodsCubeOnSlotRequest(Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			GoodsCubeSlot goodsCubeSlot = _logicMapStateManager.LogicMapState.CubeSlotInfo[goodsCubeSlotGuid].Slot;
			GoodsCube cube = _logicMapStateManager.ProduceGoodsCubeInSlot(goodsCubeSlot, goodsColor);
			_sceneStateEvents.RaiseGoodsCubeProducedInSlotEvent(goodsCubeSlot, cube);
		}

		private void RemoveGoodsCubeFromSlotRequest(Guid goodsCubeSlotGuid)
		{
			GoodsCubeSlot goodsCubeSlot = _logicMapStateManager.LogicMapState.CubeSlotInfo[goodsCubeSlotGuid].Slot;
			_logicMapStateManager.RemoveCube(goodsCubeSlot.GoodsCube);
			_sceneStateEvents.RaiseGoodsCubeRemovedFromSlotEvent(goodsCubeSlot);
		}

		private void TransitionToStateMachineRequest(IStateMachine stateMachine)
		{
			_stateMachineManager.NextState(stateMachine);
		}

		private void PlayCardRequest(Guid cardId)
		{
			_logicCardStateManager.PlayActionFromCard(cardId);
		}

		private void PreviousStateRequest()
		{
			_stateMachineManager.PreviousState();
		}
	}
}
