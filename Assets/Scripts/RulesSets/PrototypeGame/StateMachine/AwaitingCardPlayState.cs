using CardSystem;
using GameEngine.Commands;
using GameEngine.StateMachine;
using PrototypeGame.Commands;
using PrototypeGame.UI;
using System;
using UnityEngine;

namespace PrototypeGame.StateMachine
{
	internal class AwaitingCardPlayState : IStateMachine
	{
		private CardServices _cardServices;
		private UserInterface _userInterface;

		private CommandManager _commandManager;
		private CommandFactory _commandFactory;

		public AwaitingCardPlayState(CardServices cardServices, UserInterface userInterface, CommandManager commandManager, CommandFactory commandFactory) 
		{ 
			_cardServices = cardServices;
			_userInterface = userInterface;
			_commandManager = commandManager;
			_commandFactory = commandFactory;
		}
		public void EnterState()
		{
			_cardServices.DragStartedEvent += OnDragStarted;
			_cardServices.DragEndedEvent += OnDragEnded;
			_userInterface.PlayCardDropArea.OnCardDropEvent += OnCardDropped;
			_userInterface.CurrentMessage.text = "Play a card from hand";
		}

		public void ExitState()
		{
			_cardServices.DragStartedEvent -= OnDragStarted;
			_cardServices.DragEndedEvent -= OnDragEnded;
			_userInterface.PlayCardDropArea.OnCardDropEvent -= OnCardDropped;
			_userInterface.CurrentMessage.text = "";
		}

		private void OnDragStarted()
		{
			_userInterface.PlayCardDropArea.gameObject.SetActive(true);
			
		}

		private void OnDragEnded()
		{
			_userInterface.PlayCardDropArea.gameObject.SetActive(false);
		}

		private void OnCardDropped(Guid cardId)
		{
			Debug.Log(cardId);
			_commandManager.PushAndExecuteCommand(_commandFactory.CreatePlayCardActionCommand(cardId));
		}
	}
}
