using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    public abstract class CardBehaviorBase : ScriptableObject
    {
        public virtual void StartBehavior() { }
        public virtual void EndBehavior() { }
    }
}