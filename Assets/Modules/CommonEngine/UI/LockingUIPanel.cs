using CommonEngine.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CommonEngine.UI
{
	/// <summary>
	/// UI element that adds itself to the InputLock upon entering
	/// </summary>
	public class LockingUIPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField]
		protected CommonServices _commonServices;

		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			_commonServices.InputLock.AddLock(gameObject);
		}

		public virtual void OnPointerExit(PointerEventData eventData)
		{
			_commonServices.InputLock.RemoveLock(gameObject);
		}
	}
}
