using CardSystem;
using CommonEngine.Core;
using CommonEngine.Events;
using CommonEngine.UI.Options;
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
		private OptionPanel _optionPanel;
		[SerializeField]
		private CardServices _cardServices;

		[SerializeField]
		private Button _undoButton;
		[SerializeField]
		private Button _confirmButton;

		private IRulesSet _rules;

		void Start()
		{
			Setup(new PrototypeRulesSet(_commonServices, _gameServices, _optionPanel, _cardServices));
			_undoButton?.onClick.AddListener(Undo);
			_confirmButton?.onClick.AddListener(Confirm);
		}

		private void OnDestroy()
		{
			_rules.StopFlow();
			_undoButton?.onClick.RemoveListener(Undo);
			_confirmButton?.onClick.RemoveListener(Confirm);
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

		private void Confirm()
		{
			_rules.Confirm();
		}

	}
}