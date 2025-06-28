using CommonEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CommonEngine.UI
{
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
