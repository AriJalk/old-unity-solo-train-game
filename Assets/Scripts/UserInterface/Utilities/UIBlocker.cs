using Engine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SoloTrainGame.UI
{
    public class UIBlocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool CanBlock;
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (CanBlock)
            {
                ServiceLocator.UserInterfaceService?.AddBlocker(this);
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            ServiceLocator.UserInterfaceService?.RemoveBlocker(this);
        }
    }
}