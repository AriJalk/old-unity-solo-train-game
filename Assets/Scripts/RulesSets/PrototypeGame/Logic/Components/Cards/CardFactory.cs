using TurnBasedHexEngine.Commands;
using PrototypeGame.Commands;
using PrototypeGame.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeGame.Logic.Components.Cards
{
	internal class CardFactory
	{
		private CommandManager _commandManager;
		private CommandFactory _commandFactory;
		public CardFactory()
		{

		}

		public void Initialize(CommandManager commandManager, CommandFactory commandFactory)
		{
			_commandManager = commandManager;
			_commandFactory = commandFactory;
		}
		
		// Todo: from SO
		public BuildActionCard CreateBasicBuildActionCard()
		{
			BuildActionCard card = new BuildActionCard(Guid.NewGuid(), "Basic Build", 2, 2, "Basic Build Action", _commandManager, _commandFactory);

			return card;
		}

		public TransportActionCard CreateTransportActionCard()
		{
			TransportActionCard card = new TransportActionCard(Guid.NewGuid(), "Transport", 2, 2, "Basic Transport Action", _commandManager, _commandFactory);

			return card;
		}
	}
}
