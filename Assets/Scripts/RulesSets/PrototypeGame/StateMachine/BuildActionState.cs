using CommonEngine.Core;
using GameEngine.Commands;
using GameEngine.StateMachine;
using PrototypeGame.Commands;
using PrototypeGame.Logic.Components.Cards;
using PrototypeGame.Logic.Services;
using PrototypeGame.Scene;
using PrototypeGame.StateMachine.CommonStates;
using PrototypeGame.UI;
using System;
using UnityEngine;

namespace PrototypeGame.StateMachine
{
	internal class BuildActionState : IStateMachine
	{
		const string STATE_MESSAGE = "Build Action, {0}$ remaining\nSelect tile to build on, or play another card to add $";
		private int _availableMoney;
		private CommonServices _commonServices;
		private UserInterface _userInterface;

		private CommandManager _commandManager;
		private CommandFactory _commandFactory;

		private CardDragAndDropState _cardDragAndDropState;

		private ICardLookupService _cardLookupService;

		public BuildActionState(CommonServices commonServices, CommandManager commandManager, CommandFactory commandFactory, UserInterface userInterface, CardDragAndDropState cardDragAndDropState, ICardLookupService cardLookupService, int availableMoney)
		{
			_commonServices = commonServices;
			_commandManager = commandManager;
			_commandFactory = commandFactory;
			_userInterface = userInterface;
			_cardDragAndDropState = cardDragAndDropState;
			_cardLookupService = cardLookupService;
			_availableMoney = availableMoney;
		}

		public void EnterState()
		{
			_cardDragAndDropState.OnDropHandler = OnCardDrop;
			_cardDragAndDropState.EnterState();
			_userInterface.CurrentMessage.text = string.Format(STATE_MESSAGE, _availableMoney);
			_commonServices.RaycastConfig.SetRaycastLayer(typeof(HexTileObject));
			_commonServices.CommonEngineEvents.ColliderSelectedEvent += OnColliderSelected;
		}

		public void ExitState()
		{
			_cardDragAndDropState.ExitState();
			_commonServices.CommonEngineEvents.ColliderSelectedEvent -= OnColliderSelected;
			_userInterface.CurrentMessage.text = "";
		}

		private void OnColliderSelected(RaycastHit hit)
		{
			if (hit.collider.GetComponent<HexTileObject>() is HexTileObject tile)
			{
				_commandManager.NextCommandGroup();
				ICommand command = _commandFactory.CreateBuildStationCommand(tile.HexCoord);
				_commandManager.PushAndExecuteCommand(command);
			}
		}

		private void OnCardDrop(Guid cardId)
		{
			ProtoCardData cardData = _cardLookupService.GetCardData(cardId);
			if (cardData != null)
			{
				_commandManager.NextCommandGroup();
				_availableMoney += cardData.MoneyValue;
				_userInterface.CurrentMessage.text = string.Format(STATE_MESSAGE, _availableMoney);

				//Discard
				_commandManager.PushAndExecuteCommand(_commandFactory.CreateRemoveCardFromHandCommand(cardId));
			}
		}
	}
}
