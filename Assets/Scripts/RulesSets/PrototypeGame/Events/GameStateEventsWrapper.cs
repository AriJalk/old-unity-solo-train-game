namespace PrototypeGame.Events
{
	/// <summary>
	/// Wrapper for State Events
	/// </summary>
	internal class GameStateEventsWrapper
	{
		public readonly SceneMapEvents SceneMapEvents = new SceneMapEvents();
		public readonly SceneCardEvents SceneCardEvents = new SceneCardEvents();
	}
}
