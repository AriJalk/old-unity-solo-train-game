using CommonEngine.Interfaces;
using PrototypeGame.Commands;
using System;
using TurnBasedHexEngine.Commands;

namespace PrototypeGame.Logic.Components.Cards
{
	internal class ProtoCardData : IIdentifiable
	{
		protected readonly CommandManager _commandManager;
		protected readonly CommandFactory _commandFactory;

		public Guid guid { get; private set; }
		public string CardTitle {  get; private set; }
		public int MoneyValue { get; private set; }
		public int TransportPointsValue { get; private set; }
		public string CardDescription { get; private set; }

		public bool CanBeDiscarded { get; private set; }
		public bool IsAlwaysLastInHand { get; private set; }


		public ProtoCardData(Guid guid, string cardTitle, int moneyValue, int transportPointsValue, string cardDescription, CommandManager commandManager, CommandFactory commandFactory, bool canBeDiscarded = true, bool isAlwaysLastInHand = false)
		{
			this.guid = guid;
			CardTitle = cardTitle;
			MoneyValue = moneyValue;
			TransportPointsValue = transportPointsValue;
			CardDescription = cardDescription;

			_commandManager = commandManager;
			_commandFactory = commandFactory;
			CanBeDiscarded = canBeDiscarded;
			IsAlwaysLastInHand = isAlwaysLastInHand;
		}

		public virtual void PlayAction() { }
	}
}
