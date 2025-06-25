using CommonEngine.Core;
using CommonEngine.Events;
using GameEngine.Core;
using GameEngine.Map;
using UnityEngine;
using UnityEngine.UI;


namespace PrototypeGame
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private HexGridController _gridController;
		[SerializeField]
		private CommonServices _commonServices;
		[SerializeField]
		private GameEngineServices _gameServices;

		[SerializeField]
		private Button _undoButton;

		private IRulesSet _rules;

		void Start()
		{
			Setup(new PrototypeRulesSet(_commonServices, _gameServices));
			_undoButton.onClick.AddListener(Undo);
		}

		private void OnDestroy()
		{
			_rules.StopFlow();
			_undoButton.onClick.RemoveListener(Undo);
		}

		void Setup(IRulesSet rulesSet)
		{
			_rules = rulesSet;
			_rules.Setup();
			_rules.StartFlow();
		}

		private void Undo()
		{
			_rules.Undo();
		}

	}
}