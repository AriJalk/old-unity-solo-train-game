using PrototypeGame.Logic;
using PrototypeGame.Events;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
using System;

namespace PrototypeGame.Scene.State
{
	/// <summary>
	/// Main scene object management class, listens to events from SceneStateEvents and dispatches relevant methods in SceneStateManipulator
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
		}

		private void OnTileBuilt(HexTileData tileData)
		{
			HexTileObject tile =_sceneStateManipulator.BuildTile(tileData);
			_hexGridController.AddTileToGrid(tile);
		}

		private void OnTransportCube(Guid originSlot, Guid destinationSlot)
		{
			GoodsCubeSlotObject origin = _sceneGameState.CubeSlots[originSlot];	
			GoodsCubeSlotObject destination = _sceneGameState.CubeSlots[destinationSlot];
			_sceneStateManipulator.TransportGoodsCube(origin, destination);
		}

		private void OnFactoryBuilt(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneGameState.Tiles[hexTileData.HexCoord];
			_sceneStateManipulator.BuildFactoryOnTile(hexTileObject, hexTileData.Factory);
		}

		private void OnFactoryRemoved(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneGameState.Tiles[hexTileData.HexCoord];
			_sceneStateManipulator.RemoveFactoryFromTile(hexTileObject);
		}

		public void OnStationBuilt(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneGameState.Tiles[hexTileData.HexCoord];
			_sceneStateManipulator.BuildStationOnTile(hexTileObject, hexTileData.Station);
		}

		public void OnStationRemoved(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _sceneGameState.Tiles[hexTileData.HexCoord];
			_sceneStateManipulator.RemoveStationFromTile(hexTileObject);
		}
	}
}