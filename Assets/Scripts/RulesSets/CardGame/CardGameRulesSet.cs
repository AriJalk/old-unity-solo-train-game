using CardGame.GameBuilder;
using CardGame.Logic;
using CardGame.Logic.Services;
using CardGame.Scene.Services;
using CardGame.Services;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
using UnityEngine;


namespace CardGame
{
	public class CardGameRulesSet : IRulesSet
	{
		private CommonServices _commonServices;
		private GameEngineServices _gameEngineServices;

		private GameStateServices _gameStateServices;
		private LogicManager _logicManager;
		private SceneManager _sceneManager;

		public CardGameRulesSet(CommonServices commonServices, GameEngineServices gameEngineServices)
		{
			_commonServices = commonServices;
			_gameEngineServices = gameEngineServices;
			_gameStateServices = new GameStateServices();
			_logicManager = new LogicManager(new LogicGameState());
			_sceneManager = new SceneManager(_commonServices, gameEngineServices, _gameStateServices);
		}

		public void Setup()
		{
			Builder.Build(_commonServices, _gameEngineServices, _gameStateServices);
			_commonServices.CommonConfig.RaycastLayer = _commonServices.CommonConfig.RaycastLayers[typeof(GoodsCubeObject)];
		}


		public void StartFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent += ColliderHit;
			return;
		}


		public void StopFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent -= ColliderHit;

			_sceneManager.Dispose();
		}


		private void ColliderHit(RaycastHit hit)
		{
			if (hit.collider.GetComponent<HexTileObjectBase>() is HexTileObjectBase tile)
			{
				Debug.Log(tile.HexCoord);
			}
			if (hit.collider.GetComponent<GoodsCubeObject>() is GoodsCubeObject cube)
			{
				Debug.Log(cube.guid);
			}
		}
	}
}
