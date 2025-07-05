using PrototypeGame.Commands;
using System;
using TurnBasedHexEngine.Commands;

namespace PrototypeGame.Logic.Components.Cards
{
	internal class RetrieveAndProduceCard : ProtoCardData
	{
		public RetrieveAndProduceCard(Guid guid, string cardTitle, int moneyValue, int transportPointsValue, string cardDescription, CommandManager commandManager, CommandFactory commandFactory) : base(guid, cardTitle, moneyValue, transportPointsValue, cardDescription, commandManager, commandFactory, false, true)
		{
		}

		public override void PlayAction()
		{
			base.PlayAction();

			ICommand command = _commandFactory.CreateRemoveCardFromHandCommand(guid);
			//_commandManager.PushAndExecuteCommand(command);

			command = _commandFactory.CreateRetreiveCardsFromDiscardCommand();
			_commandManager.PushAndExecuteCommand(command);

			command = _commandFactory.CreateProduceGoodsInAllFactorySlotsCommand();
			_commandManager.PushAndExecuteCommand(command);
		}
	}
}
