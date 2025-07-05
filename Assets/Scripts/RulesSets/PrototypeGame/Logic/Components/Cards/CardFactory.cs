using PrototypeGame.Commands;
using System;
using TurnBasedHexEngine.Commands;


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
			BuildActionCard card = new BuildActionCard(Guid.NewGuid(), "Build", 2, 2, "Basic Build Action", _commandManager, _commandFactory);

			return card;
		}

		public TransportActionCard CreateBasicTransportActionCard()
		{
			TransportActionCard card = new TransportActionCard(Guid.NewGuid(), "Transport", 2, 2, "Basic Transport Action", _commandManager, _commandFactory);

			return card;
		}

		public RetrieveAndProduceCard CreateRetrieveAndProduceCard()
		{
			RetrieveAndProduceCard card = new RetrieveAndProduceCard(Guid.NewGuid(), "Retrieve", 0, 0, "Retrieve Cards and Produce", _commandManager, _commandFactory);

			return card;
		}
	}
}
