using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SoloTrainGame.UI
{
    public class UIElementClickable : UIBlocker, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent<UIElementClickable> ElementClickedEvent;

        private bool _isButtonDown;

        private void Awake()
        {
            ElementClickedEvent = new UnityEvent<UIElementClickable>();
        }
        public override void OnPointerEnter(PointerEventData eventData) 
        {
            base.OnPointerEnter(eventData);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            _isButtonDown = false;
        }
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _isButtonDown = true;
        }
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (_isButtonDown && Input.GetMouseButtonUp(0))
            {
                ElementClickedEvent.Invoke(this);
                _isButtonDown = false;
            }
        }
    }

}