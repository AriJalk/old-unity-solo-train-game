namespace SoloTrainGame.GameLogic
{
    public interface IGameAction
    {
        bool CanExecute {  get; }
        void Execute();
        void Undo();
    }
}