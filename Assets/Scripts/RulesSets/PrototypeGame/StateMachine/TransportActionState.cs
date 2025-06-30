using CommonEngine.Core;
using PrototypeGame.Commands;
using PrototypeGame.Events.CommandRequestEvents;
using PrototypeGame.Logic.Components.Cards;
using PrototypeGame.Logic.Services;
using PrototypeGame.Scene;
using PrototypeGame.StateMachine.CommonStates;
using PrototypeGame.UI;
using System;
using TurnBasedHexEngine.Commands;
using TurnBasedHexEngine.StateMachine;
using UnityEngine;

namespace PrototypeGame.StateMachine
{
	// TODO: extract state commons for DRY
	internal class TransportActionState : IStateMachine
	{
		const string STATE_MESSAGE = "Transport Action, {0}TP remaining\nSelect tile to build on, or play another card to add TP";
		private int _transportPoints;

		private CommonServices _commonServices;
		private UserInterface _userInterface;

		private CommandManager _commandManager;
		private CommandFactory _commandFactory;

		private CardDragAndDropState _cardDragAndDropState;

		private ICardLookupService _cardLookupService;

		private CardCommandRequestEvents _cardCommandRequestEvents;



		private GoodsCubeSlotObject _sourceSlot;
		private GoodsCubeSlotObject _destinationSlot;
		public TransportActionState(CommonServices commonServices, CommandManager commandManager, CommandFactory commandFactory, UserInterface userInterface, CardDragAndDropState cardDragAndDropState, ICardLookupService cardLookupService, CardCommandRequestEvents cardCommandRequestEvents, int transportPoints) 
		{
			_commonServices = commonServices;
			_commandManager = commandManager;
			_commandFactory = commandFactory;
			_userInterface = userInterface;
			_cardDragAndDropState = cardDragAndDropState;
			_cardLookupService = cardLookupService;
			_cardCommandRequestEvents = cardCommandRequestEvents;
			_transportPoints = transportPoints;
		}
		public void EnterState()
		{
			_cardDragAndDropState.OnDropHandler = OnCardDrop;
			_cardDragAndDropState.EnterState();
			_userInterface.CurrentMessage.text = string.Format(STATE_MESSAGE, _transportPoints);
			_commonServices.RaycastConfig.SetRaycastLayer(typeof(GoodsCubeSlotObject));
			_commonServices.CommonEngineEvents.ColliderSelectedEvent += OnColliderSelected;
			_cardCommandRequestEvents.MoveCardFromDiscardToHandRequestEvent += OnMoveCardFromDiscardToHandRequestEvent;
		}

		public void ExitState()
		{
			_cardDragAndDropState.ExitState();
			_commonServices.CommonEngineEvents.ColliderSelectedEvent -= OnColliderSelected;
			_userInterface.CurrentMessage.text = "";
			_cardCommandRequestEvents.MoveCardFromDiscardToHandRequestEvent -= OnMoveCardFromDiscardToHandRequestEvent;
		}

		public void OnColliderSelected(RaycastHit hit)
		{
			if (hit.collider.GetComponentInParent<GoodsCubeSlotObject>() is GoodsCubeSlotObject slot)
			{
				if (_sourceSlot == null && slot.GoodsCubeObject != null)
				{
					_sourceSlot = slot;
				}
				else if (_sourceSlot != null && _sourceSlot != slot && slot.GoodsCubeObject == null)
				{
					_destinationSlot = slot;
				}
			}
			if (_sourceSlot != null && _destinationSlot != null)
			{
				_commandManager.NextCommandGroup();
				ICommand command = _commandFactory.CreateTransportCubeCommand(_sourceSlot.guid, _destinationSlot.guid);
				_commandManager.PushAndExecuteCommand(command);
				_sourceSlot = null;
				_destinationSlot = null;
			}
		}

		private void OnCardDrop(Guid cardId)
		{
			ProtoCardData cardData = _cardLookupService.GetCardData(cardId);
			if (cardData != null)
			{
				_commandManager.NextCommandGroup();
				_transportPoints += cardData.TransportPointsValue;
				_userInterface.CurrentMessage.text = string.Format(STATE_MESSAGE, _transportPoints);

				//Discard
				_commandManager.PushAndExecuteCommand(_commandFactory.CreateRemoveCardFromHandCommand(cardId));
			}
		}

		private void OnMoveCardFromDiscardToHandRequestEvent(Guid cardId)
		{
			ProtoCardData cardData = _cardLookupService.GetCardData(cardId);
			if (cardData != null)
			{
				_transportPoints -= cardData.TransportPointsValue;
				_userInterface.CurrentMessage.text = string.Format(STATE_MESSAGE, _transportPoints);
			}
		}

	}
}
