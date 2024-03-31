using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    public abstract class CardBehaviorSO : ScriptableObject
    {
        public virtual void StartBehavior(CardSO cardSO) { }
        public virtual void EndBehavior() { }


    }
}