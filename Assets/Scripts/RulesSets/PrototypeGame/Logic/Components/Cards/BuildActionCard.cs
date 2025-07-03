using PrototypeGame.Commands;
using System;
using TurnBasedHexEngine.Commands;

namespace PrototypeGame.Logic.Components.Cards
{
	internal class BuildActionCard : ProtoCardData
	{
		public BuildActionCard(Guid guid, string cardTitle, int moneyValue, int transportPointsValue, string cardDescription, CommandManager commandManager, CommandFactory commandFactory) : base(guid, cardTitle, moneyValue, transportPointsValue, cardDescription, commandManager, commandFactory)
		{
		}

		public override void PlayAction()
		{
			base.PlayAction();
			ICommand command = _commandFactory.CreateTransitionToBuildStateCommand(MoneyValue);
			_commandManager.PushAndExecuteCommand(command);
		}
	}
}
