using CardSystem;
using TurnBasedHexEngine.Commands;
using TurnBasedHexEngine.StateMachine;
using PrototypeGame.Commands;
using PrototypeGame.StateMachine.CommonStates;
using PrototypeGame.UI;
using System;
using UnityEngine;
using PrototypeGame.RulesServices;
using PrototypeGame.StateMachine.StateServices;

namespace PrototypeGame.StateMachine
{
	internal class AwatingPlayCardForActionState : IStateMachine
	{
		private UserInterface _userInterface;

		private CommandManager _commandManager;
		private CommandFactory _commandFactory;

		private CardDragAndDropState _cardDragAndDropState;

		private RulesValidator _rulesValidator;

		public AwatingPlayCardForActionState(CoreStateDependencies coreStateDependencies, CardDragAndDropState cardDragAndDropState) 
		{ 
			_userInterface = coreStateDependencies.UserInterface;
			_commandManager = coreStateDependencies.CommandManager;
			_commandFactory = coreStateDependencies.CommandFactory;
			_rulesValidator = coreStateDependencies.RulesValidator;
			_cardDragAndDropState = cardDragAndDropState;
		}
		public void EnterState()
		{
			_cardDragAndDropState.EnterState();
			_cardDragAndDropState.OnCardDroppedEvent += OnCardDropped;
			_userInterface.CurrentMessage.text = "Play a card from hand for its action";
		}

		public void ExitState()
		{
			_userInterface.CurrentMessage.text = "";
			_cardDragAndDropState.OnCardDroppedEvent -= OnCardDropped;
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
