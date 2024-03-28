namespace SoloTrainGame.Core
{
    public interface IGameState
    {
        void OnEnterGameState();
        void OnExitGameState();
    }
}