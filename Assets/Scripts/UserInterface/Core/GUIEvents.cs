using UnityEngine.Events;

namespace SoloTrainGame.UI
{
    public class GUIEvents
    {
        public UnityEvent<CardUIObject> CardClicked {  get; private set; }

        public GUIEvents()
        {
            CardClicked = new UnityEvent<CardUIObject>();
        }

    }

}