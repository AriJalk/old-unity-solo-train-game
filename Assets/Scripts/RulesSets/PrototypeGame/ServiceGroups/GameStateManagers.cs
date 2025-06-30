using CardSystem;
using CommonEngine.Core;
using TurnBasedHexEngine.Commands;
using TurnBasedHexEngine.Core;
using TurnBasedHexEngine.StateMachine;
using PrototypeGame.Events;
using PrototypeGame.Logic.State;
using PrototypeGame.Logic.State.Cards;
using PrototypeGame.Scene.State;
using PrototypeGame.Scene.State.Cards;
using System;

namespace PrototypeGame.ServiceGroups
{
	// Main API for all state service providers
	internal class GameStateManagers : IDisposable
	{
		public LogicMapStateManager LogicMapStateManager { get; private set; }
		public SceneMapStateManager SceneMapStateManager { get; private set; }
		
		public LogicCardStateManager LogicCardStateManager { get; private set; }
		public SceneCardStateManager SceneCardStateManager { get; private set; }
		public CommandManager CommandManager { get; private set; }
		public StateMachineManager StateMachineManager { get; private set; }

		public GameStateManagers(CommonServices commonServices, GameEngineServices gameEngineServices, LogicMapState logicMapState, SceneEventsWrapper sceneEventsWrapper, LogicCardState logicCardState, CardObjectServices cardServices, StateMachineManager stateMachineManager)
		{
			LogicMapStateManager = new LogicMapStateManager(logicMapState);
			SceneMapStateManager = new SceneMapStateManager(commonServices, gameEngineServices, sceneEventsWrapper);
			LogicCardStateManager = new LogicCardStateManager(logicCardState);
			SceneCardStateManager = new SceneCardStateManager(commonServices, cardServices, sceneEventsWrapper);

			StateMachineManager = stateMachineManager;
		}

		public void Dispose()
		{
			SceneMapStateManager.Dispose();
			SceneCardStateManager.Dispose();
		}
	}
}
