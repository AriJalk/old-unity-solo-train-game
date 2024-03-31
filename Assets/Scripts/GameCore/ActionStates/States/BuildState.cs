namespace SoloTrainGame.Core
{
    public class BuildState : IActionState
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