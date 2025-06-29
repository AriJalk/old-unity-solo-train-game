using PrototypeGame.GameBuilder;
using PrototypeGame.Logic.State;
using PrototypeGame.Events;
using CommonEngine.Core;
using GameEngine.Core;
using UnityEngine;
using PrototypeGame.Scene;
using CommonEngine.UI.Options;
using PrototypeGame.UI.Options;
using System.Collections.Generic;
using HexSystem;
using CardSystem;
using PrototypeGame.Logic.State.Cards;
using PrototypeGame.UI;
using PrototypeGame.ServiceGroups;
using GameEngine.Commands;
using GameEngine.StateMachine;
using PrototypeGame.Commands;


namespace PrototypeGame
{
	public class PrototypeRulesSet : IRulesSet
	{
		private CommonServices _commonServices;
		private GameEngineServices _gameEngineServices;
		private UserInterface _userInterface;


		private StateManagers _stateManagers;

		private CardServices _cardServices;

		private OptionPanel _optionPanel;

		private CommandManager _commandManager;
		private StateMachineManager _stateMachineManager;

		private CommandFactory _commandFactory;
		private StateMachineFactory _stateMachineFactory;

		// Test variabled
		private List<BuildingOption> _buildingOptions;
		private HexCoord _buildCoord;



		public PrototypeRulesSet(CommonServices commonServices, GameEngineServices gameEngineServices, OptionPanel optionPanel, CardServices cardServices, UserInterface userInterface)
		{
			_commonServices = commonServices;
			_gameEngineServices = gameEngineServices;
			_cardServices = cardServices;
			_userInterface = userInterface;

			_stateManagers = new StateManagers(commonServices, gameEngineServices, new LogicMapState(), new GameStateEvents(), new LogicCardState(), _cardServices);

			_commandManager = new CommandManager();

			_stateMachineManager = new StateMachineManager();

			_optionPanel = optionPanel;

			_commandFactory = new CommandFactory(_stateManagers.CommandRequestEvents);
			_stateMachineFactory = new StateMachineFactory(_commandManager, _userInterface, _cardServices);
		}


		public void Setup()
		{
			ResourceLoader.LoadResources(_commonServices);
			Builder.Build(_stateManagers.GameStateEvents, _stateManagers.LogicMapStateManager, _stateManagers.LogicCardStateManager);
			_commonServices.RaycastConfig.SetRaycastLayer<HexTileObject>();
		}


		public void StartFlow()
		{
			_commonServices.CommonEngineEvents.ColliderSelectedEvent += ColliderHit;
			_commandManager.NextCommandGroup();
			_stateMachineManager.NextState(_stateMachineFactory.CreateAwatingCardPlayState());
		}


		public void StopFlow()
		{
			_commonServices.CommonEngineEvents.ColliderSelectedEvent -= ColliderHit;

			_stateManagers.Dispose();
			_stateMachineManager.ExitHeadState();
		}

		public void Undo()
		{
			_commandManager.UndoCommandGroup();
		}

		public void Confirm()
		{
			_commandManager.NextCommandGroup();
		}


		private void ColliderHit(RaycastHit hit)
		{
			/*
			// Test interactions
			if (hit.collider.GetComponent<HexTileObject>() is HexTileObject tile)
			{
				GameObject prefab = Resources.Load<GameObject>("Prefabs/PrototypeGame/UI/BuildingOption");
				_buildingOptions = new List<BuildingOption>();
				if (_logicMapStateManager.LogicMapState.Tiles[tile.HexCoord].Factory == null)
				{
					BuildingOption factoryOption = GameObject.Instantiate(prefab).GetComponent<BuildingOption>();
					factoryOption.Setup(Guid.NewGuid(), "Factory", true);
					_buildingOptions.Add(factoryOption);
				}
				if (_logicMapStateManager.LogicMapState.Tiles[tile.HexCoord].Station == null)
				{
					BuildingOption stationOption = GameObject.Instantiate(prefab).GetComponent<BuildingOption>();
					stationOption.Setup(Guid.NewGuid(), "Station", true);
					_buildingOptions.Add(stationOption);
				}
				if (_buildingOptions.Count > 0)
				{
					_buildCoord = tile.HexCoord;
					_optionPanel.OpenPanel(_buildingOptions);
					_optionPanel.OptionSelectedEvent += HandleOptions;
				}
			}
			if (hit.collider.GetComponent<GoodsCubeObject>() is GoodsCubeObject cube)
			{
				Debug.Log(cube.guid);
			}*/
		}

		/*
		private void HandleOptions(Guid guid)
		{
			// Test options building placement commands
			_optionPanel.OptionSelectedEvent -= HandleOptions;
			_optionPanel.ClosePanel();
			BuildingOption option = _buildingOptions.FirstOrDefault(b => b.guid == guid);
			if (option != null)
			{
				switch (option.BuildingName)
				{
					case "Factory":
						_commandManager.NextCommandGroup();
						_commandManager.CreateAndExecuteBuildFactoryCommand(_buildCoord, GoodsColor.GREEN);

						HexTileData tile = _logicMapStateManager.LogicMapState.Tiles[_buildCoord];
						_commandManager.CreateAndExecuteProduceGoodsCubeInSlotCommand(tile.Factory.GoodsCubeSlot.guid, tile.Factory.ProductionColor);

						_commandManager.NextCommandGroup();
						break;
					case "Station":
						_commandManager.CreateAndExecuteBuildStationCommand(_buildCoord);
						break;
				}
			}
		}
		*/
	}
}
