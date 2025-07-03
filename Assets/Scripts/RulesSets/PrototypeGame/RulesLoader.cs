using CardSystem;
using CommonEngine.Core;
using CommonEngine.UI.Options;
using PrototypeGame;
using PrototypeGame.Scene;
using PrototypeGame.UI;
using System;
using System.Collections.Generic;
using TurnBasedHexEngine.Core;
using TurnBasedHexEngine.Map;
using UnityEngine;

namespace RulesSets.PrototypeGame
{
	internal class RulesLoader : MonoBehaviour
	{
		[SerializeField]
		private GameSceneManager _gameSceneManager;
		[SerializeField]
		private GameEngineServices _gameEngineServices;
		[SerializeField]
		private CommonServices _commonServices;
		[SerializeField]
		private CardObjectServices _cardObjectServices;
		[SerializeField]
		private UserInterface _userInterface;


		public void Start()
		{
			_commonServices.RaycastConfig.SetTypeMappings(new Dictionary<Type, int>()
			{
				{typeof(HexTileObject), 1 << 6 },
				{typeof(GoodsCubeObject), 1 << 7 },
				{typeof(GoodsCubeSlotObject), 1 << 8 }});
			PrototypeRulesSet rulesSet = new PrototypeRulesSet(_commonServices, _gameEngineServices, _cardObjectServices, _userInterface);
			_gameSceneManager.Setup(rulesSet);
			Destroy(this.gameObject);
		}
	}
}
