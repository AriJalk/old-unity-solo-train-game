using CommonEngine.UI.Options;
using PrototypeGame.UI.CardSystem;
using TMPro;
using UnityEngine;

namespace PrototypeGame.UI
{
	public class UserInterface : MonoBehaviour
	{
		[SerializeField]
		private OptionPanel optionPanel;

		public TextMeshProUGUI CurrentMessage;

		public PlayCardDropArea PlayCardDropArea;
	}
}
