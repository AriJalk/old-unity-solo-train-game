using PrototypeGame.Commands;
using PrototypeGame.Logic.Components.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnBasedHexEngine.Commands;

namespace Assets.Scripts.RulesSets.PrototypeGame.Logic.Components.Cards
{
	internal class RetreiveCard : ProtoCardData
	{
		public RetreiveCard(Guid guid, string cardTitle, int moneyValue, int transportPointsValue, string cardDescription, CommandManager commandManager, CommandFactory commandFactory) : base(guid, cardTitle, moneyValue, transportPointsValue, cardDescription, commandManager, commandFactory, false)
		{
		}

		public override void PlayAction()
		{
			base.PlayAction();

			ICommand command = _commandFactory.CreateRetreiveCardsFromDiscardCommand();

			_commandManager.PushAndExecuteCommand(command);
		}
	}
}
