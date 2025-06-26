using CommonEngine.UI.Options;
using TMPro;
using UnityEngine;


namespace PrototypeGame.UI.Options
{
	internal class BuildingOption : OptionObject
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		public void Setup(string text, bool isEnabled = false)
		{
			_text.text = text;
			_button.enabled = isEnabled;
		}
	}
}
