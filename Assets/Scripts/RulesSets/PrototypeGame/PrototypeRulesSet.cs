using PrototypeGame.GameBuilder;
using PrototypeGame.Logic.State;
using PrototypeGame.Events;
using CommonEngine.Core;
using TurnBasedHexEngine.Core;
using UnityEngine;
using PrototypeGame.Scene;
using CardSystem;
using PrototypeGame.Logic.State.Cards;
using PrototypeGame.UI;
using PrototypeGame.ServiceGroups;
using TurnBasedHexEngine.Commands;
using TurnBasedHexEngine.StateMachine;
using PrototypeGame.Commands;
using PrototypeGame.Logic.Components.Cards;
using PrototypeGame.StateMachine;
using PrototypeGame.RulesServices;


namespace PrototypeGame
{
	public class PrototypeRulesSet : IRulesSet
	{
		private CommonServices _commonServices;
		private GameEngineServices _gameEngineServices;
		private UserInterface _userInterface;

		private CardObjectServices _cardServices;

		private CommandManager _commandManager;

		private GameStateManagers _gameStateManagers;
		private SceneEventsWrapper _sceneEventsWrapper;
		private CommandRequestEventsWrapper _commandRequestEventsWrapper;
		private CommandEventHandlersWrapper _commandEventHandlersWrapper;

		private CommandFactory _commandFactory;
		private StateMachineFactory _stateMachineFactory;
		private CardFactory _cardFactory;

		

		// Test variables
		//private List<BuildingOption> _buildingOptions;
		//private HexCoord _buildCoord;



		public PrototypeRulesSet(CommonServices commonServices, GameEngineServices gameEngineServices, CardObjectServices cardServices, UserInterface userInterface)
		{
			_commonServices = commonServices;
			_gameEngineServices = gameEngineServices;
			_cardServices = cardServices;
			_userInterface = userInterface;
			

			_sceneEventsWrapper = new SceneEventsWrapper();

			_gameStateManagers = new GameStateManagers(commonServices, _gameEngineServices, new LogicMapState(), _sceneEventsWrapper, new LogicCardState(), _cardServices, new StateMachineManager());

			_commandManager = new CommandManager();

			_commandRequestEventsWrapper = new CommandRequestEventsWrapper();

			_commandEventHandlersWrapper = new CommandEventHandlersWrapper(_gameStateManagers, _commandRequestEventsWrapper, _sceneEventsWrapper);

			_stateMachineFactory = new StateMachineFactory();
			_commandFactory = new CommandFactory();
			_cardFactory = new CardFactory();


			_stateMachineFactory.Initialize(_commandManager, _userInterface, _cardServices, _commandFactory, _commonServices, _gameStateManagers.LogicCardStateManager, _commandRequestEventsWrapper, new RulesValidator(_gameStateManagers.LogicCardStateManager.LogicCardState, _gameStateManagers.LogicMapStateManager.LogicMapState));
			_commandFactory.Initialize(_commandRequestEventsWrapper, _stateMachineFactory, _gameStateManagers.StateMachineManager, _gameStateManagers.LogicCardStateManager);
			_cardFactory.Initialize(_commandManager, _commandFactory);
		}


		public void Setup()
		{
			ResourceLoader.LoadResources(_commonServices);
			Builder.Build(_sceneEventsWrapper, _gameStateManagers.LogicMapStateManager, _gameStateManagers.LogicCardStateManager, _cardFactory);
			_commonServices.RaycastConfig.SetRaycastLayer<HexTileObject>();
		}


		public void StartFlow()
		{
			_commonServices.CommonEngineEvents.ColliderSelectedEvent += ColliderHit;
			_commandManager.NextCommandGroup();
			_gameStateManagers.StateMachineManager.NextState(_stateMachineFactory.CreateAwatingPlayCardForActionState());
		}


		public void StopFlow()
		{
			_commonServices.CommonEngineEvents.ColliderSelectedEvent -= ColliderHit;

			_gameStateManagers.Dispose();
			_gameStateManagers.StateMachineManager.CurrentState?.ExitState();
			_commandEventHandlersWrapper.Dispose();
		}

		public void Undo()
		{
			_commandManager.UndoCommandGroup();
		}

		public void Confirm()
		{
			_commandManager.NextCommandGroup();
			_commandManager.PushAndExecuteCommand(_commandFactory.CreateTransitionToAwatingPlayCardForActionCommand());
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
