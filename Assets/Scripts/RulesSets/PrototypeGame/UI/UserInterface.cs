using CommonEngine.UI.Options;
using PrototypeGame.UI.CardSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame.UI
{
	public class UserInterface : MonoBehaviour
	{
		public OptionsPanel OptionsPanel;

		public TextMeshProUGUI CurrentMessage;

		public PlayCardDropTarget PlayCardDropTarget;

		[SerializeField]
		private Button _undoButton;
		[SerializeField]
		private Button _confirmButton;

		public void EnableButtons()
		{
			_undoButton.enabled = true;
			_confirmButton.enabled = true;
		}

		public void DisableButtons()
		{
			_undoButton.enabled = false;
			_confirmButton.enabled = false;
		}
	}
}
