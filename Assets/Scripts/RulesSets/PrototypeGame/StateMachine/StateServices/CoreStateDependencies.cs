using CommonEngine.Core;
using PrototypeGame.Commands;
using PrototypeGame.RulesServices;
using PrototypeGame.StateMachine.CommonStates;
using PrototypeGame.UI;
using TurnBasedHexEngine.Commands;

namespace PrototypeGame.StateMachine.StateServices
{
	internal class CoreStateDependencies
	{
		public CommonServices CommonServices { get; private set; }
		public UserInterface UserInterface { get; private set; }
		public CommandManager CommandManager { get; private set; }
		public CommandFactory CommandFactory { get; private set; }
		public RulesValidator RulesValidator { get; private set; }

		public CoreStateDependencies(CommonServices commonServices, UserInterface userInterface, CommandManager commandManager, CommandFactory commandFactory, RulesValidator rulesValidator)
		{
			CommonServices = commonServices;
			UserInterface = userInterface;
			CommandManager = commandManager;
			CommandFactory = commandFactory;
			RulesValidator = rulesValidator;
		}
	}
}
