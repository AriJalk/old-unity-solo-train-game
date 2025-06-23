using CardGame.GameBuilder;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.Map;
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
		}


		public void StartFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent?.AddListener(ColliderHit);
			return;
		}


		public void StopFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent?.RemoveListener(ColliderHit);
		}


		private void ColliderHit(RaycastHit hit)
		{
			if (hit.collider.GetComponent<HexTileObject>() is HexTileObject tile)
			{
				Debug.Log(tile.HexCoord);
			}
		}
	}
}
