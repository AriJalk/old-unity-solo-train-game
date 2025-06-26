using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CommonEngine.UI.OptionSelection
{
	public class OptionObject : MonoBehaviour, IIdentifiable
	{
		[SerializeField]
		private Button _button;

		public Guid guid { get; set; }

		public event Action<Guid> SelectedEvent;

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
