using CardSystem;
using GameEngine.Commands;
using GameEngine.StateMachine;
using PrototypeGame.Commands;
using PrototypeGame.StateMachine.CommonStates;
using PrototypeGame.UI;
using System;
using UnityEngine;

namespace PrototypeGame.StateMachine
{
	internal class AwatingPlayCardForAction : IStateMachine
	{
		private UserInterface _userInterface;

		private CommandManager _commandManager;
		private CommandFactory _commandFactory;

		private CardDragAndDropState _cardDragAndDropState;

		public AwatingPlayCardForAction(UserInterface userInterface, CommandManager commandManager, CommandFactory commandFactory, CardDragAndDropState cardDragAndDropState) 
		{ 
			_userInterface = userInterface;
			_commandManager = commandManager;
			_commandFactory = commandFactory;
			_cardDragAndDropState = cardDragAndDropState;
		}
		public void EnterState()
		{
			_cardDragAndDropState.OnDropHandler = OnCardDropped;
			_cardDragAndDropState.EnterState();
			_userInterface.CurrentMessage.text = "Play a card from hand";
		}

		public void ExitState()
		{
			_userInterface.CurrentMessage.text = "";
			_cardDragAndDropState.ExitState();
		}


		private void OnCardDropped(Guid cardId)
		{
			_commandManager.NextCommandGroup();
			_commandManager.PushAndExecuteCommand(_commandFactory.CreatePlayCardActionCommand(cardId));

			//Discard
			_commandManager.PushAndExecuteCommand(_commandFactory.CreateRemoveCardFromHandCommand(cardId));
		}
	}
}
