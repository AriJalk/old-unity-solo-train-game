using CommonEngine.Core;
using TurnBasedHexEngine.Commands;
using TurnBasedHexEngine.StateMachine;
using PrototypeGame.Commands;
using PrototypeGame.Events.CommandRequestEvents;
using PrototypeGame.Logic.Components.Cards;
using PrototypeGame.Scene;
using PrototypeGame.StateMachine.CommonStates;
using PrototypeGame.UI;
using System;
using UnityEngine;
using PrototypeGame.RulesServices;
using PrototypeGame.Logic.ServiceContracts;
using CommonEngine.UI.Options;
using System.Collections.Generic;
using HexSystem;
using PrototypeGame.UI.Options;

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

		private CardCommandRequestEvents _cardCommandRequestEvents;

		private RulesValidator _rulesValidator;

		#region Options
		private HexCoord _selectedTileCoord;
		private OptionsPanel _optionsPanel;
		private Dictionary<Guid, BuildingType> _buildingOptions;
		#endregion

		public BuildActionState(CommonServices commonServices, CommandManager commandManager, CommandFactory commandFactory, UserInterface userInterface, CardDragAndDropState cardDragAndDropState, ICardLookupService cardLookupService, CardCommandRequestEvents cardCommandRequestEvents, RulesValidator rulesValidator, int availableMoney)
		{
			_commonServices = commonServices;
			_commandManager = commandManager;
			_commandFactory = commandFactory;
			_userInterface = userInterface;
			_cardDragAndDropState = cardDragAndDropState;
			_cardLookupService = cardLookupService;
			_cardCommandRequestEvents = cardCommandRequestEvents;
			_rulesValidator = rulesValidator;
			_availableMoney = availableMoney;
			_optionsPanel = _userInterface.OptionsPanel;
		}

		public void EnterState()
		{
			_cardDragAndDropState.OnDropHandler = OnCardDrop;
			_cardDragAndDropState.EnterState();
			_userInterface.CurrentMessage.text = string.Format(STATE_MESSAGE, _availableMoney);
			_commonServices.RaycastConfig.SetRaycastLayer(typeof(HexTileObject));
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

		private void OnColliderSelected(RaycastHit hit)
		{
			if (hit.collider.GetComponent<HexTileObject>() is HexTileObject tile)
			{
				_selectedTileCoord = tile.HexCoord;
				CreateBuildingOptions();
			}
		}

		private void OnCardDrop(Guid cardId)
		{
			ProtoCardData cardData = _cardLookupService.GetCardData(cardId);
			if (cardData != null && cardData.CanBeDiscarded)
			{
				_commandManager.NextCommandGroup();
				_availableMoney += cardData.MoneyValue;
				_userInterface.CurrentMessage.text = string.Format(STATE_MESSAGE, _availableMoney);

				//Discard
				_commandManager.PushAndExecuteCommand(_commandFactory.CreateRemoveCardFromHandCommand(cardId));
			}
		}

		private void OnMoveCardFromDiscardToHandRequestEvent(Guid cardId)
		{
			ProtoCardData cardData = _cardLookupService.GetCardData(cardId);
			if (cardData != null)
			{
				_availableMoney -= cardData.MoneyValue;
				_userInterface.CurrentMessage.text = string.Format(STATE_MESSAGE, _availableMoney);
			}
		}

		private void CreateBuildingOptions()
		{
			_buildingOptions = new Dictionary<Guid, BuildingType>();
			GameObject optionPrefab = Resources.Load<GameObject>("Prefabs/PrototypeGame/UI/BuildingOption");
			List<BuildingOption> options = new List<BuildingOption>();
			BuildingOption option;
			Guid guid;

			if (_rulesValidator.IsValidBuildLocation(_selectedTileCoord, BuildingType.STATION))
			{
				guid = Guid.NewGuid();
				option = GameObject.Instantiate(optionPrefab).GetComponent<BuildingOption>();
				option.Setup(guid, "Station", true);
				options.Add(option);
				_buildingOptions.Add(guid, BuildingType.STATION);
			}
			if (_rulesValidator.IsValidBuildLocation(_selectedTileCoord, BuildingType.FACTORY))
			{
				guid = Guid.NewGuid();
				option = GameObject.Instantiate(optionPrefab).GetComponent<BuildingOption>();
				option.Setup(guid, "Factory", true);
				options.Add(option);
				_buildingOptions.Add(guid, BuildingType.FACTORY);
			}

			if (options.Count > 0)
			{
				_optionsPanel.OpenPanel(options);
				_optionsPanel.OptionSelectedEvent += OnBuildingOptionSelected;
			}

		}

		private void OnBuildingOptionSelected(Guid guid)
		{
			_optionsPanel.OptionSelectedEvent -= OnBuildingOptionSelected;
			_optionsPanel.ClosePanel();
			List<ICommand> commands = new List<ICommand>();

			switch (_buildingOptions[guid])
			{
				case BuildingType.STATION:
					commands.Add(_commandFactory.CreateBuildStationCommand(_selectedTileCoord));
					break;

				case BuildingType.FACTORY:
					commands.Add(_commandFactory.CreateBuildFactoryCommand(_selectedTileCoord, GoodsColor.BLUE));
					break;
			}

			if (commands.Count > 0)
			{
				_commandManager.NextCommandGroup();
				foreach (ICommand command in commands)
				{
					_commandManager.PushAndExecuteCommand(command);
				}
			}
			_selectedTileCoord = null;
			_buildingOptions = null;
		}
	}
}
