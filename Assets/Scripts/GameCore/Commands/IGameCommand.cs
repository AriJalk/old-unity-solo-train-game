namespace SoloTrainGame.Core
{
    public interface IGameCommand
    {
        bool CanExecute {  get; }
        void Execute();
        void Undo();
    }
}