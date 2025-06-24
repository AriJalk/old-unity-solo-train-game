using CardGame.Logic;
using CardGame.Services;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
using System;

namespace CardGame.Scene.Services
{
	internal class SceneManager : IDisposable
	{
		private SceneGameState _sceneGameState;
		private GameStateEvents _gameStateEvents;
		private SceneStateManipulator _sceneStateManipulator;
		private HexGridController _hexGridController;

		public SceneManager(CommonServices commonServices, GameEngineServices gameEngineServices, GameStateServices gameStateServices)
		{
			_sceneGameState = new SceneGameState();
			_gameStateEvents = gameStateServices.GameStateEvents;
			_sceneStateManipulator = new SceneStateManipulator(commonServices, _sceneGameState);
			_hexGridController = gameEngineServices.HexGridController;

			_gameStateEvents.TileBuiltEvent += OnTileBuilt;
			_gameStateEvents.TransportCubeEvent += OnTransportCube;
		}

		public void Dispose()
		{
			_gameStateEvents.TileBuiltEvent -= OnTileBuilt;
			_gameStateEvents.TransportCubeEvent -= OnTransportCube;
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
	}
}