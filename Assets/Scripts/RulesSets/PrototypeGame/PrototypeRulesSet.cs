using PrototypeGame.GameBuilder;
using PrototypeGame.Logic;
using PrototypeGame.Logic.MetaData;
using PrototypeGame.Logic.Services;
using PrototypeGame.Scene.Services;
using PrototypeGame.Services;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
using HexSystem;
using UnityEngine;
using PrototypeGame.Scene;
using PrototypeGame.Commands;


namespace PrototypeGame
{
	public class PrototypeRulesSet : IRulesSet
	{
		private CommonServices _commonServices;
		private GameEngineServices _gameEngineServices;

		private LogicStateManager _logicStateManager;
		private CommandEventHandler _commandEventHandler;

		private GameStateEvents _gameStateServices;
		private SceneManager _sceneManager;



		private CommandManager _commandManager;

		public PrototypeRulesSet(CommonServices commonServices, GameEngineServices gameEngineServices)
		{
			_commonServices = commonServices;
			_gameEngineServices = gameEngineServices;
			_gameStateServices = new GameStateEvents();
			_logicStateManager = new LogicStateManager(new LogicGameState());
			_sceneManager = new SceneManager(_commonServices, gameEngineServices, _gameStateServices);
			_commandManager = new CommandManager(_gameStateServices.LogicStateEvents);
			_commandEventHandler = new CommandEventHandler(_logicStateManager, _gameStateServices);
		}

		public void Setup()
		{
			ResourceLoader.LoadResources(_commonServices);
			Builder.Build(_gameStateServices, _logicStateManager);
			_commonServices.RaycastConfig.SetRaycastLayer<HexTileObject>();
		}


		public void StartFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent += ColliderHit;
		}


		public void StopFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent -= ColliderHit;

			_sceneManager.Dispose();
			_commandEventHandler.Dispose();
		}

		public void Undo()
		{
			_commandManager.UndoCommandGroup();
		}

		private void ColliderHit(RaycastHit hit)
		{
			if (hit.collider.GetComponent<HexTileObject>() is HexTileObject tile)
			{
				Debug.Log(tile.HexCoord);

				if (_logicStateManager.LogicGameState.Tiles[tile.HexCoord].Factory == null)
				{
					_commandManager.StartCommandGroup();
					_commandManager.CreateAndExecuteFactoryBuildCommand(tile.HexCoord, GoodsColor.GREEN);
					_commandManager.EndCommandGroup();
				}
			}
			if (hit.collider.GetComponent<GoodsCubeObject>() is GoodsCubeObject cube)
			{
				Debug.Log(cube.guid);
			}
		}
	}
}
