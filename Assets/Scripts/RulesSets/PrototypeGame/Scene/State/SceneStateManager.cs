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

		private SceneStateEvents _gameStateEvents;

		private SceneStateManipulator _sceneStateManipulator;

		private HexGridController _hexGridController;

		public SceneStateManager(CommonServices commonServices, GameEngineServices gameEngineServices, GameStateEvents gameStateServices)
		{
			_sceneGameState = new SceneGameState();
			_gameStateEvents = gameStateServices.SceneStateEvents;
			_sceneStateManipulator = new SceneStateManipulator(commonServices, _sceneGameState);
			_hexGridController = gameEngineServices.HexGridController;

			_gameStateEvents.TileBuiltEvent += OnTileBuilt;
			_gameStateEvents.TransportCubeEvent += OnTransportCube;
			_gameStateEvents.FactoryBuiltEvent += OnFactoryBuilt;
			_gameStateEvents.FactoryRemovedEvent += OnFactoryRemoved;
		}

		public void Dispose()
		{
			_gameStateEvents.TileBuiltEvent -= OnTileBuilt;
			_gameStateEvents.TransportCubeEvent -= OnTransportCube;
			_gameStateEvents.FactoryBuiltEvent -= OnFactoryBuilt;
			_gameStateEvents.FactoryRemovedEvent -= OnFactoryRemoved;
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
	}
}