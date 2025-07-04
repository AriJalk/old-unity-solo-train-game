namespace TurnBasedHexEngine.Commands
{
	/// <summary>
	/// Schema for logic layer commands to be pushed into CommandManager. ideally commands are the only authority to trigger the chain reaction to manipulate the game state through request events which are handled in the logic and scene layer
	/// </summary>
	public interface ICommand
	{
		void Execute();
		void Undo();
	}
}
