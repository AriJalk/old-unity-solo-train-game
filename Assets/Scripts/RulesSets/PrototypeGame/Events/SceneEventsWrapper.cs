namespace PrototypeGame.Events
{
	/// <summary>
	/// Wrapper for Scene Events
	/// </summary>
	internal class SceneEventsWrapper
	{
		public readonly SceneMapEvents SceneMapEvents = new SceneMapEvents();
		public readonly SceneCardEvents SceneCardEvents = new SceneCardEvents();
	}
}
