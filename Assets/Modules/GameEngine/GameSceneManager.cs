using CardGame;
using CommonEngine.Core;
using CommonEngine.Events;
using GameEngine.Core;
using GameEngine.Map;
using UnityEngine;


namespace GameEngine
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private HexGridController _gridController;
		[SerializeField]
		private CommonServices _commonServices;
		[SerializeField]
		private GameServices _gameServices;

		private SceneEvents _sceneEvents;

		private IRulesSet _rules;

		void Start()
		{
			_rules = new CardGameRulesSet(_commonServices, _gameServices);
			Setup(_rules);
		}

		private void OnDestroy()
		{
			_rules.StopFlow();
		}

		void Setup(IRulesSet rulesSet)
		{
			_rules = rulesSet;
			_rules.Setup();
			_rules.StartFlow();
		}

	}
}