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


namespace PrototypeGame
{
	public class PrototypeRulesSet : IRulesSet
	{
		private CommonServices _commonServices;
		private GameEngineServices _gameEngineServices;

		private GameStateServices _gameStateServices;
		private LogicStateManager _logicManager;
		private SceneManager _sceneManager;

		public PrototypeRulesSet(CommonServices commonServices, GameEngineServices gameEngineServices)
		{
			_commonServices = commonServices;
			_gameEngineServices = gameEngineServices;
			_gameStateServices = new GameStateServices();
			_logicManager = new LogicStateManager(new LogicGameState());
			_sceneManager = new SceneManager(_commonServices, gameEngineServices, _gameStateServices);
		}

		public void Setup()
		{
			ResourceLoader.LoadResources(_commonServices);
			Builder.Build(_gameStateServices, _logicManager);
			_commonServices.RaycastConfig.SetRaycastLayer<GoodsCubeObject>();
		}


		public void StartFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent += ColliderHit;

			//Test transportation
			HexTileData zero = _logicManager.LogicGameState.Tiles[HexCoord.GetCoord(0, 0)];

			foreach (SlotInfo slotInfo in _logicManager.LogicGameState.CubeSlotInfo.Values)
			{
				GoodsCubeSlot slot = slotInfo.Slot;
				if (slot.GoodsCube != null)
				{
					_logicManager.TransportGoodsCube(slot, zero.Station.GoodsCubeSlot1);
					_gameStateServices.GameStateEvents.RaiseTransportCubeEvent(slot.guid, zero.Station.GoodsCubeSlot1.guid);
					break;
				}
			}
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
