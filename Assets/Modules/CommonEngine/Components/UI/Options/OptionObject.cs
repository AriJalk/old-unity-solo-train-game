using CommonEngine.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CommonEngine.Componenets.UI.Options
{
	/// <summary>
	/// General purpose option for OptionsPanel
	/// </summary>
	public class OptionObject : MonoBehaviour, IIdentifiable
	{
		public event Action<Guid> SelectedEvent;

		[SerializeField]
		protected Button _button;

		public Guid guid { get; set; }


		private void Start()
		{
			_button.onClick?.AddListener(RaiseSelectedEvent);
		}

		private void OnDestroy()
		{
			_button?.onClick?.RemoveListener(RaiseSelectedEvent);
		}

		protected void RaiseSelectedEvent()
		{
			SelectedEvent?.Invoke(guid);
		}
	}
}
