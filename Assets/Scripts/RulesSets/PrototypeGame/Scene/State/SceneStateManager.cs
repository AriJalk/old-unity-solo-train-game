using PrototypeGame.Logic;
using PrototypeGame.Events;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
using System;

namespace PrototypeGame.Scene.State
{
	/// <summary>
	/// Main scene object management class, stores the scene-state, listens to events from SceneStateEvents and dispatches relevant methods in SceneStateManipulator
	/// *** Only Authority on modifying SceneGameState ***
	/// </summary>
	internal class SceneStateManager : IDisposable
	{
		private SceneGameState _sceneGameState;

		private SceneStateEvents _sceneStateEvents;

		private SceneStateManipulator _sceneStateManipulator;

		private HexGridController _hexGridController;

		public SceneStateManager(CommonServices commonServices, GameEngineServices gameEngineServices, GameStateEvents gameStateServices)
		{
			_sceneGameState = new SceneGameState();
			_sceneStateEvents = gameStateServices.SceneStateEvents;
			_sceneStateManipulator = new SceneStateManipulator(commonServices, _sceneGameState);
			_hexGridController = gameEngineServices.HexGridController;

			_sceneStateEvents.TileBuiltEvent += OnTileBuilt;

			_sceneStateEvents.TransportCubeEvent += OnTransportCube;
			
			_sceneStateEvents.FactoryBuiltEvent += OnFactoryBuilt;
			_sceneStateEvents.FactoryRemovedEvent += OnFactoryRemoved;

			_sceneStateEvents.GoodsCubeProducedInSlotEvent += OnGoodsCubeProducedInSlot;
			_sceneStateEvents.GoodsCubeRemovedFromSlotEvent += OnGoodsCubeRemovedFromSlot;

			_sceneStateEvents.StationBuiltEvent += OnStationBuilt;
			_sceneStateEvents.StationRemovedEvent += OnStationRemoved;

		}

		public void Dispose()
		{
			_sceneStateEvents.TileBuiltEvent -= OnTileBuilt;

			_sceneStateEvents.TransportCubeEvent -= OnTransportCube;

			_sceneStateEvents.FactoryBuiltEvent -= OnFactoryBuilt;
			_sceneStateEvents.FactoryRemovedEvent -= OnFactoryRemoved;

			_sceneStateEvents.StationBuiltEvent -= OnStationBuilt;
			_sceneStateEvents.StationRemovedEvent -= OnStationRemoved;

			_sceneStateEvents.GoodsCubeProducedInSlotEvent -= OnGoodsCubeProducedInSlot;
			_sceneStateEvents.GoodsCubeRemovedFromSlotEvent -= OnGoodsCubeRemovedFromSlot;
		}

		#region HelperMethod

		private void RegistersGoodCubeObject(GoodsCubeObject goodsCubeObject)
		{
			_sceneGameState.Cubes.Add(goodsCubeObject.guid, goodsCubeObject);
		}

		private void UnregisterGoodsCubeObject(GoodsCubeObject goodsCubeObject)
		{
			_sceneGameState.Cubes.Remove(goodsCubeObject.guid);
			_sceneGameState.CubeToSlot.Remove(goodsCubeObject.guid);
		}

		private void RegisterGoodsCubeSlotObject(GoodsCubeSlotObject goodsCubeSlotObject)
		{
			_sceneGameState.CubeSlots.Add(goodsCubeSlotObject.guid, goodsCubeSlotObject);
		}

		private void UnregisterGoodsCubeSlotObject(GoodsCubeSlotObject goodsCubeSlotObject)
		{
			_sceneGameState.CubeSlots.Remove(goodsCubeSlotObject.guid);
		}

		#endregion

		#region EventHandlers

		private void OnTileBuilt(HexTileData tileData)
		{
			HexTileObject tileObject =_sceneStateManipulator.BuildTile(tileData);
			_sceneGameState.Tiles.Add(tileObject.HexCoord, tileObject);
			_hexGridController.AddTileToGrid(tileObject);
		}

		private void OnTransportCube(Guid originSlot, Guid destinationSlot)
		{
			GoodsCubeSlotObject origin = _sceneGameState.CubeSlots[originSlot];
			GoodsCubeSlotObject destination = _sceneGameState.CubeSlots[destinationSlot];
			_sceneGameState.CubeToSlot[origin.GoodsCubeObject.guid] = destination.guid;
			_sceneStateManipulator.TransportGoodsCube(origin, destination);
		}

		private void OnFactoryBuilt(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneGameState.Tiles[hexTileData.HexCoord];
			FactoryObject factoryObject = _sceneStateManipulator.BuildFactoryObject(hexTileData.Factory);
			factoryObject.GoodsCubeSlotObject.guid = hexTileData.Factory.GoodsCubeSlot.guid;
			RegisterGoodsCubeSlotObject(factoryObject.GoodsCubeSlotObject);
			_sceneStateManipulator.AttachFactoryToTile(factoryObject, hexTileObject);
		}

		private void OnFactoryRemoved(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneGameState.Tiles[hexTileData.HexCoord];
			UnregisterGoodsCubeSlotObject(hexTileObject.FactoryObject.GoodsCubeSlotObject);
			_sceneStateManipulator.DetachFactoryFromTile(hexTileObject);
		}

		public void OnStationBuilt(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneGameState.Tiles[hexTileData.HexCoord];
			StationObject stationObject = _sceneStateManipulator.BuildStationObject(hexTileData.Station);
			stationObject.GoodsCubeSlotObject1.guid = hexTileData.Station.GoodsCubeSlot1.guid;
			RegisterGoodsCubeSlotObject(stationObject.GoodsCubeSlotObject1);
			stationObject.GoodsCubeSlotObject2.guid = hexTileData.Station.GoodsCubeSlot2.guid;
			RegisterGoodsCubeSlotObject(stationObject.GoodsCubeSlotObject2);
			_sceneStateManipulator.AttachStationToTile(stationObject, hexTileObject);
		}

		public void OnStationRemoved(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneGameState.Tiles[hexTileData.HexCoord];
			UnregisterGoodsCubeSlotObject(hexTileObject.StationObject.GoodsCubeSlotObject1);
			UnregisterGoodsCubeSlotObject(hexTileObject.StationObject.GoodsCubeSlotObject2);
			_sceneStateManipulator.DetachStationFromTile(hexTileObject);
		}

		public void OnGoodsCubeProducedInSlot(GoodsCube goodsCube, Guid goodsCubeSlotId)
		{
			GoodsCubeSlotObject goodsCubeSlotObject = _sceneGameState.CubeSlots[goodsCubeSlotId];
			GoodsCubeObject goodsCubeObject = _sceneStateManipulator.BuildGoodsCubeObject(goodsCube);
			RegistersGoodCubeObject(goodsCubeObject);
			_sceneGameState.CubeToSlot[goodsCubeObject.guid] = goodsCubeSlotObject.guid;
			_sceneStateManipulator.AttachGoodsCubeToSlot(goodsCubeObject, goodsCubeSlotObject);
		}

		public void OnGoodsCubeRemovedFromSlot(Guid goodsCubeSlotId)
		{
			GoodsCubeSlotObject goodsCubeSlotObject = _sceneGameState.CubeSlots[goodsCubeSlotId];
			UnregisterGoodsCubeObject(goodsCubeSlotObject.GoodsCubeObject);
			_sceneStateManipulator.DetachGoodsCubeFromSlot(goodsCubeSlotObject);
		}
		#endregion
	}
}