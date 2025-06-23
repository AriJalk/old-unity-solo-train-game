using CardGame.GameBuilder;
using CardGame.Scene;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
using Unity.VisualScripting;
using UnityEngine;


namespace CardGame
{
	public class CardGameRulesSet : IRulesSet
	{
		private CommonServices _commonServices;
		private GameServices _gameServices;

		public CardGameRulesSet(CommonServices serviceLocator, GameServices gameServices)
		{
			_commonServices = serviceLocator;
			_gameServices = gameServices;
		}

		public void Setup()
		{
			Builder.Build(_commonServices, _gameServices);
			_commonServices.CommonConfig.RaycastLayer = _commonServices.CommonConfig.RaycastLayers[typeof(HexTileObject)];
		}


		public void StartFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent += ColliderHit;
			return;
		}


		public void StopFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent -= ColliderHit;
		}


		private void ColliderHit(RaycastHit hit)
		{
			if (hit.collider.GetComponent<HexTileObjectBase>() is HexTileObjectBase tile)
			{
				Debug.Log(tile.HexCoord);
			}
			if (hit.collider.transform.parent.GetComponent<GoodsCubeSlotObject>() is GoodsCubeSlotObject slot)
			{
				Debug.Log("SLOT");
			}
		}
	}
}
