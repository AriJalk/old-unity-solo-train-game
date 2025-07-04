namespace TurnBasedHexEngine.StateMachine
{
	/// <summary>
	/// StateMachine objects are responsible for setting the game context depending on the rules flow. Only authority to dispatch commands to maniuplate the game state during gameplay.
	/// </summary>
	public interface IStateMachine
	{
		void EnterState();
		void ExitState();
	}
}
