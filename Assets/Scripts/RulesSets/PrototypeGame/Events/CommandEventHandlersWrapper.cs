using PrototypeGame.Events.CommandEventHandlers;
using PrototypeGame.ServiceGroups;
using System;

namespace PrototypeGame.Events
{
	/// <summary>
	/// Orchestrating logic + scene command execution
	/// </summary>
	internal class CommandEventHandlersWrapper : IDisposable
	{
		private MapCommandEventsHandler _mapCommandEventsHandler;
		private CardCommandEventsHandler _cardCommandEventsHandler;
		private StateCommandEventsHandler _stateCommandEventsHandler;

		public CommandEventHandlersWrapper(GameStateManagers gameStateManagers, CommandRequestEventsWrapper commandRequestEventsWrapper, SceneEventsWrapper sceneEventsWrapper)
		{
			_mapCommandEventsHandler = new MapCommandEventsHandler(gameStateManagers.LogicMapStateManager, sceneEventsWrapper.SceneMapEvents, commandRequestEventsWrapper.MapCommandRequestEvents);

			_cardCommandEventsHandler = new CardCommandEventsHandler(gameStateManagers.LogicCardStateManager, commandRequestEventsWrapper.CardCommandRequestEvents, sceneEventsWrapper.SceneCardEvents);

			_stateCommandEventsHandler = new StateCommandEventsHandler(gameStateManagers.StateMachineManager, commandRequestEventsWrapper.StateCommandRequestEvents);
		}

		public void Dispose()
		{
			_mapCommandEventsHandler.Dispose();
			_cardCommandEventsHandler.Dispose();
			_stateCommandEventsHandler.Dispose();
		}
	}
}
