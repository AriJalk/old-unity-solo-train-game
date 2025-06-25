namespace PrototypeGame.Events
{
	/// <summary>
	/// Wrapper for State Events
	/// </summary>
	internal class GameStateEvents
	{
		public readonly SceneStateEvents SceneStateEvents = new SceneStateEvents();
		public readonly LogicStateEvents LogicStateEvents = new LogicStateEvents();
	}
}
