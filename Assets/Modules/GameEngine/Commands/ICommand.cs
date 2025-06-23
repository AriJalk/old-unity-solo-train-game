
namespace GameEngine.Commands
{
	public interface ICommand
	{
		void Execute();
		void Undo();
	}

}
