namespace TurnBasedHexEngine.Commands
{
	/// <summary>
	/// Schema for commands to be pushed into CommandManager
	/// </summary>
	public interface ICommand
	{
		void Execute();
		void Undo();
	}
}
