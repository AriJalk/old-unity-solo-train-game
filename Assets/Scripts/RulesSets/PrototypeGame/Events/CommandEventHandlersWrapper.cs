using GameEngine.StateMachine;
using HexSystem;
using PrototypeGame.Events.CommandEventHandlers;
using PrototypeGame.Logic;
using PrototypeGame.Logic.MetaData;
using PrototypeGame.Logic.State;
using PrototypeGame.Logic.State.Cards;
using PrototypeGame.ServiceGroups;
using System;
using System.Diagnostics;

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

		public CommandEventHandlersWrapper(GameStateManagers gameStateManagers, CommandRequestEventsWrapper commandRequestEventsWrapper, GameStateEventsWrapper gameStateEventsWrapper)
		{
			_mapCommandEventsHandler = new MapCommandEventsHandler(gameStateManagers.LogicMapStateManager, gameStateEventsWrapper.SceneMapEvents, commandRequestEventsWrapper.MapCommandRequestEvents);

			_cardCommandEventsHandler = new CardCommandEventsHandler(gameStateManagers.LogicCardStateManager, commandRequestEventsWrapper.CardCommandRequestEvents);

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
