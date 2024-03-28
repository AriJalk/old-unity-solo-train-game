using SoloTrainGame.Core;

namespace SoloTrainGame.GameLogic
{
    public class BuildState : IGameState
    {
        public int AvailableMoney { get; private set; }
        public BuildState(int availableMoney)
        {
            AvailableMoney = availableMoney;
        }

        public void AddMoney(int amount)
        {
            if (amount > 0)
                AvailableMoney += amount;
        }

        public void RemoveMoney(int amount)
        {
            if (amount > 0)
                AvailableMoney -= amount;
        }

        public void OnEnterGameState()
        {
            
        }

        public void OnExitGameState()
        {
            
        }
    }
}