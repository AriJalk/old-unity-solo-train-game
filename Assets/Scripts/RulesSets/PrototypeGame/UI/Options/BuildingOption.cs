using CommonEngine.UI.OptionSelection;
using TMPro;
using UnityEngine;


namespace PrototypeGame.UI.Options
{
	internal class BuildingOption : OptionObject
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		public void Setup(string text)
		{
			_text.text = text;
		}
	}
}
