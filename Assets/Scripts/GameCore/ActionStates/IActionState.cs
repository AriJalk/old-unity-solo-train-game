namespace SoloTrainGame.Core
{
    public interface IActionState
    {
        void OnEnterGameState();
        void OnExitGameState();
    }
}