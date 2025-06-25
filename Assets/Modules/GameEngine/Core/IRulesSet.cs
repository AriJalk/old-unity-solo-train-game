namespace GameEngine.Core
{
	/// <summary>
	/// A unified (logic + tree) rules set to be injected into the scene manager
	/// </summary>
	public interface IRulesSet
	{
		/// <summary>
		/// Call from the GameSceneManager after scene is loaded
		/// </summary>
		void Setup();

		/// <summary>
		/// Call to start the game
		/// </summary>
		void StartFlow();

		/// <summary>
		/// Call at end of session
		/// </summary>
		void StopFlow();

		void Undo();

		void Confirm();
	}
}
