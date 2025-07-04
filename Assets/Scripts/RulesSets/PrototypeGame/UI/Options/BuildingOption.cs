using CommonEngine.Componenets.UI.Options;
using System;
using TMPro;
using UnityEngine;


namespace PrototypeGame.UI.Options
{
	internal class BuildingOption : OptionObject
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		public string BuildingName { get; private set; }

		public void Setup(Guid guid, string text, bool isEnabled = false)
		{
			this.guid = guid;
			_text.text = text;
			_button.enabled = isEnabled;
			BuildingName = text;
		}
	}
}
