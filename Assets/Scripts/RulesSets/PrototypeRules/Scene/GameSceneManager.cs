using CommonEngine.Core;
using CommonEngine.Events;
using GameEngine.Core;
using GameEngine.Map;
using UnityEngine;


namespace PrototypeGame.Scene
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private HexGridController _gridController;
		[SerializeField]
		private CommonServices _commonServices;
		[SerializeField]
		private GameEngineServices _gameServices;

		private SceneEvents _sceneEvents;

		private IRulesSet _rules;

		void Start()
		{
			Setup(new PrototypeRulesSet(_commonServices, _gameServices));
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