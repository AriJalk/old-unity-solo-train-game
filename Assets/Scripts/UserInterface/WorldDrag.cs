using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace SoloTrainGame.UI
{
    public class WorldDrag : MonoBehaviour, IDragHandler
    {
        public UnityEvent<Vector2> OnDragEvent;

        private void Awake()
        {
            
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void OnDestroy()
        {
            OnDragEvent.RemoveAllListeners();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize()
        {
            OnDragEvent = new UnityEvent<Vector2>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent?.Invoke(eventData.delta);
        }

    }

}
