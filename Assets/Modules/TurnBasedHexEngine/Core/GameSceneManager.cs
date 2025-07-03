using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedHexEngine.Core
{
	public class GameSceneManager : MonoBehaviour
	{
		[SerializeField]
		private Button _undoButton;
		[SerializeField]
		private Button _confirmButton;

		private IRulesSet _rules;

		void Start()
		{
			Application.targetFrameRate = 60;
			_undoButton?.onClick.AddListener(Undo);
			_confirmButton?.onClick.AddListener(Confirm);
		}

		private void OnDestroy()
		{
			_rules.StopFlow();
			_undoButton?.onClick.RemoveListener(Undo);
			_confirmButton?.onClick.RemoveListener(Confirm);
		}

		public void Setup(IRulesSet rulesSet)
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