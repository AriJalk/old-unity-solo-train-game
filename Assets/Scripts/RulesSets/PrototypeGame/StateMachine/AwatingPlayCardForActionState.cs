using CardSystem;
using TurnBasedHexEngine.Commands;
using TurnBasedHexEngine.StateMachine;
using PrototypeGame.Commands;
using PrototypeGame.StateMachine.CommonStates;
using PrototypeGame.UI;
using System;
using UnityEngine;
using PrototypeGame.RulesServices;

namespace PrototypeGame.StateMachine
{
	internal class AwatingPlayCardForActionState : IStateMachine
	{
		private UserInterface _userInterface;

		private CommandManager _commandManager;
		private CommandFactory _commandFactory;

		private CardDragAndDropState _cardDragAndDropState;

		private RulesValidator _rulesValidator;

		public AwatingPlayCardForActionState(UserInterface userInterface, CommandManager commandManager, CommandFactory commandFactory, CardDragAndDropState cardDragAndDropState, RulesValidator rulesValidator) 
		{ 
			_userInterface = userInterface;
			_commandManager = commandManager;
			_commandFactory = commandFactory;
			_cardDragAndDropState = cardDragAndDropState;
			_rulesValidator = rulesValidator;
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
			if (_rulesValidator.CanCardBeDiscarded(cardId))
			{
				_commandManager.PushAndExecuteCommand(_commandFactory.CreateRemoveCardFromHandCommand(cardId));
			}
		}
	}
}
