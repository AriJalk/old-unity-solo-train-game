namespace SoloTrainGame.GameLogic
{
    public class Tracks
    {
        public bool IsUpgraded { get; private set; }

        public bool UpgradeTrack()
        {
            if (IsUpgraded == false)
            {
                IsUpgraded = true;
            }
            return false;
        }
    }
}