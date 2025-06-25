
using HexSystem;
using PrototypeGame.Services;
using System;
using UnityEngine;

namespace PrototypeGame.Logic.Services
{
	internal class CommandEventHandler : IDisposable
	{
		private LogicStateManager _logicStateManager;

		private LogicStateEvents _logicStateEvents;
		private SceneStateEvents _sceneStateEvents;

		public CommandEventHandler(LogicStateManager logicStateManager, GameStateEvents gameStateServices)
		{
			_logicStateManager = logicStateManager;
			_logicStateEvents = gameStateServices.LogicStateEvents;
			_sceneStateEvents = gameStateServices.SceneStateEvents;

			_logicStateEvents.TransportRequestEvent += TransportRequest;
			_logicStateEvents.BuildFactoryRequestEvent += BuildFactoryRequest;
			_logicStateEvents.RemoveFactoryRequestEvent += RemoveFactoryRequest;

		}

		public void Dispose()
		{
			_logicStateEvents.TransportRequestEvent -= TransportRequest;
			_logicStateEvents.BuildFactoryRequestEvent -= BuildFactoryRequest;
			_logicStateEvents.RemoveFactoryRequestEvent -= RemoveFactoryRequest;
		}

		private void TransportRequest(Guid origin, Guid destination)
		{

		}

		private void BuildFactoryRequest(HexCoord hexCoord, GoodsColor productionColor)
		{
			HexTileData hexTileData = _logicStateManager.LogicGameState.Tiles[hexCoord];

			if (hexTileData.Factory == null)
			{
				_logicStateManager.BuildFactoryOnTile(hexTileData, productionColor);
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


	}
}
