using System;
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "ScriptableObjects/CardSO", order = 1)]
    public class CardSO : ScriptableObject
    {
        public string Name;
        public string Description;
        public Enums.CardType CardType;
        public int GeneratedMoney;
        public int GeneratedTransport;
        public CardBehaviorSO CardBehavior;

        public override string ToString()
        {
            string str = "Name: " + Name;
            str += ", Description: " + Description;
            str += ", Card Type: " + CardType;
            str += ", Money: " + GeneratedMoney;
            str += ", Transport: " + GeneratedTransport;
            return str;
        }
    }

}