using PrototypeGame.Logic;
using PrototypeGame.Events;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
using System;

namespace PrototypeGame.Scene.State
{
	/// <summary>
	/// Main scene object management class, stores the scene-state, listens to events from SceneMapEvents and dispatches relevant methods in SceneMapStateManipulator
	/// *** Only Authority on modifying SceneMapState ***
	/// </summary>
	internal class SceneMapStateManager : IDisposable
	{
		private SceneMapState _sceneMapState;

		private SceneMapEvents _sceneMapEvents;

		private SceneMapStateManipulator _sceneMapStateManipulator;

		private HexGridController _hexGridController;

		public SceneMapStateManager(CommonServices commonServices, GameEngineServices gameEngineServices, GameStateEventsWrapper gameStateEvents)
		{
			_sceneMapState = new SceneMapState();
			_sceneMapEvents = gameStateEvents.SceneMapEvents;
			_sceneMapStateManipulator = new SceneMapStateManipulator(commonServices);
			_hexGridController = gameEngineServices.HexGridController;

			_sceneMapEvents.TileBuiltEvent += OnTileBuilt;

			_sceneMapEvents.TransportCubeEvent += OnTransportCube;
			
			_sceneMapEvents.FactoryBuiltEvent += OnFactoryBuilt;
			_sceneMapEvents.FactoryRemovedEvent += OnFactoryRemoved;

			_sceneMapEvents.GoodsCubeProducedInSlotEvent += OnGoodsCubeProducedInSlot;
			_sceneMapEvents.GoodsCubeRemovedFromSlotEvent += OnGoodsCubeRemovedFromSlot;

			_sceneMapEvents.StationBuiltEvent += OnStationBuilt;
			_sceneMapEvents.StationRemovedEvent += OnStationRemoved;

		}

		public void Dispose()
		{
			_sceneMapEvents.TileBuiltEvent -= OnTileBuilt;

			_sceneMapEvents.TransportCubeEvent -= OnTransportCube;

			_sceneMapEvents.FactoryBuiltEvent -= OnFactoryBuilt;
			_sceneMapEvents.FactoryRemovedEvent -= OnFactoryRemoved;

			_sceneMapEvents.StationBuiltEvent -= OnStationBuilt;
			_sceneMapEvents.StationRemovedEvent -= OnStationRemoved;

			_sceneMapEvents.GoodsCubeProducedInSlotEvent -= OnGoodsCubeProducedInSlot;
			_sceneMapEvents.GoodsCubeRemovedFromSlotEvent -= OnGoodsCubeRemovedFromSlot;
		}

		#region HelperMethod

		private void RegistersGoodCubeObject(GoodsCubeObject goodsCubeObject)
		{
			_sceneMapState.Cubes.Add(goodsCubeObject.guid, goodsCubeObject);
		}

		private void UnregisterGoodsCubeObject(GoodsCubeObject goodsCubeObject)
		{
			_sceneMapState.Cubes.Remove(goodsCubeObject.guid);
			_sceneMapState.CubeToSlot.Remove(goodsCubeObject.guid);
		}

		private void RegisterGoodsCubeSlotObject(GoodsCubeSlotObject goodsCubeSlotObject)
		{
			_sceneMapState.CubeSlots.Add(goodsCubeSlotObject.guid, goodsCubeSlotObject);
		}

		private void UnregisterGoodsCubeSlotObject(GoodsCubeSlotObject goodsCubeSlotObject)
		{
			_sceneMapState.CubeSlots.Remove(goodsCubeSlotObject.guid);
		}

		#endregion

		#region EventHandlers

		private void OnTileBuilt(HexTileData tileData)
		{
			HexTileObject tileObject =_sceneMapStateManipulator.BuildTile(tileData);
			_sceneMapState.Tiles.Add(tileObject.HexCoord, tileObject);
			_hexGridController.AddTileToGrid(tileObject);
		}

		private void OnTransportCube(Guid originSlot, Guid destinationSlot)
		{
			GoodsCubeSlotObject origin = _sceneMapState.CubeSlots[originSlot];
			GoodsCubeSlotObject destination = _sceneMapState.CubeSlots[destinationSlot];
			_sceneMapState.CubeToSlot[origin.GoodsCubeObject.guid] = destination.guid;
			_sceneMapStateManipulator.TransportGoodsCube(origin, destination);
		}

		private void OnFactoryBuilt(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneMapState.Tiles[hexTileData.HexCoord];
			FactoryObject factoryObject = _sceneMapStateManipulator.BuildFactoryObject(hexTileData.Factory);
			factoryObject.GoodsCubeSlotObject.guid = hexTileData.Factory.GoodsCubeSlot.guid;
			RegisterGoodsCubeSlotObject(factoryObject.GoodsCubeSlotObject);
			_sceneMapStateManipulator.AttachFactoryToTile(factoryObject, hexTileObject);
		}

		private void OnFactoryRemoved(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneMapState.Tiles[hexTileData.HexCoord];
			UnregisterGoodsCubeSlotObject(hexTileObject.FactoryObject.GoodsCubeSlotObject);
			_sceneMapStateManipulator.DetachFactoryFromTile(hexTileObject);
		}

		public void OnStationBuilt(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneMapState.Tiles[hexTileData.HexCoord];
			StationObject stationObject = _sceneMapStateManipulator.BuildStationObject(hexTileData.Station);
			stationObject.GoodsCubeSlotObject1.guid = hexTileData.Station.GoodsCubeSlot1.guid;
			RegisterGoodsCubeSlotObject(stationObject.GoodsCubeSlotObject1);
			stationObject.GoodsCubeSlotObject2.guid = hexTileData.Station.GoodsCubeSlot2.guid;
			RegisterGoodsCubeSlotObject(stationObject.GoodsCubeSlotObject2);
			_sceneMapStateManipulator.AttachStationToTile(stationObject, hexTileObject);
		}

		public void OnStationRemoved(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneMapState.Tiles[hexTileData.HexCoord];
			UnregisterGoodsCubeSlotObject(hexTileObject.StationObject.GoodsCubeSlotObject1);
			UnregisterGoodsCubeSlotObject(hexTileObject.StationObject.GoodsCubeSlotObject2);
			_sceneMapStateManipulator.DetachStationFromTile(hexTileObject);
		}

		public void OnGoodsCubeProducedInSlot(GoodsCube goodsCube, Guid goodsCubeSlotId)
		{
			GoodsCubeSlotObject goodsCubeSlotObject = _sceneMapState.CubeSlots[goodsCubeSlotId];
			GoodsCubeObject goodsCubeObject = _sceneMapStateManipulator.BuildGoodsCubeObject(goodsCube);
			RegistersGoodCubeObject(goodsCubeObject);
			_sceneMapState.CubeToSlot[goodsCubeObject.guid] = goodsCubeSlotObject.guid;
			_sceneMapStateManipulator.AttachGoodsCubeToSlot(goodsCubeObject, goodsCubeSlotObject);
		}

		public void OnGoodsCubeRemovedFromSlot(Guid goodsCubeSlotId)
		{
			GoodsCubeSlotObject goodsCubeSlotObject = _sceneMapState.CubeSlots[goodsCubeSlotId];
			UnregisterGoodsCubeObject(goodsCubeSlotObject.GoodsCubeObject);
			_sceneMapStateManipulator.DetachGoodsCubeFromSlot(goodsCubeSlotObject);
		}
		#endregion
	}
}