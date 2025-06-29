using CardSystem;
using CommonEngine.Core;
using GameEngine.Core;
using GameEngine.StateMachine;
using PrototypeGame.Events;
using PrototypeGame.Logic.State;
using PrototypeGame.Logic.State.Cards;
using PrototypeGame.Scene.State;
using PrototypeGame.Scene.State.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace PrototypeGame.ServiceGroups
{
	// Main API for all state service providers
	internal class StateManagers : IDisposable
	{
		public LogicMapStateManager LogicMapStateManager { get; private set; }
		public SceneMapStateManager SceneMapStateManager { get; private set; }
		
		public LogicCardStateManager LogicCardStateManager { get; private set; }
		public SceneCardStateManager SceneCardStateManager { get; private set; }

		public CommandEventHandler CommandEventHandler { get; private set; }

		public GameStateEvents GameStateEvents { get; private set; }

		public StateMachineManager StateMachineManager { get; private set; }

		public StateManagers(CommonServices commonServices, GameEngineServices gameEngineServices, LogicMapState logicMapState, GameStateEvents gameStateEvents, LogicCardState logicCardState, CardServices cardServices, StateMachineManager stateMachineManager)
		{
			GameStateEvents = gameStateEvents;
			LogicMapStateManager = new LogicMapStateManager(logicMapState);
			SceneMapStateManager = new SceneMapStateManager(commonServices, gameEngineServices, gameStateEvents);
			LogicCardStateManager = new LogicCardStateManager(logicCardState);
			SceneCardStateManager = new SceneCardStateManager(commonServices, cardServices, gameStateEvents);

			StateMachineManager = stateMachineManager;
			CommandEventHandler = new CommandEventHandler(LogicMapStateManager, gameStateEvents, StateMachineManager, LogicCardStateManager);
		}

		public void Dispose()
		{
			SceneMapStateManager.Dispose();
			SceneCardStateManager.Dispose();
			CommandEventHandler.Dispose();
		}
	}
}
