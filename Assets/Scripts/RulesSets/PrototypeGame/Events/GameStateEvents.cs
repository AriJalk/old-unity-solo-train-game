namespace PrototypeGame.Events
{
	/// <summary>
	/// Wrapper for State Events
	/// </summary>
	internal class GameStateEvents
	{
		public readonly CommandRequestEvents CommandRequestEvents = new CommandRequestEvents();
		public readonly SceneMapEvents SceneMapEvents = new SceneMapEvents();
		public readonly SceneCardEvents SceneCardEvents = new SceneCardEvents();
	}
}
