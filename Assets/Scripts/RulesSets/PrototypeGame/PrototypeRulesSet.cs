using PrototypeGame.GameBuilder;
using PrototypeGame.Logic.State;
using PrototypeGame.Scene.State;
using PrototypeGame.Events;
using CommonEngine.Core;
using GameEngine.Core;
using UnityEngine;
using PrototypeGame.Scene;
using PrototypeGame.Commands;
using CommonEngine.UI.Options;
using PrototypeGame.UI.Options;
using System.Collections.Generic;
using System;
using System.Linq;
using HexSystem;


namespace PrototypeGame
{
	public class PrototypeRulesSet : IRulesSet
	{
		private CommonServices _commonServices;
		private GameEngineServices _gameEngineServices;

		private LogicStateManager _logicStateManager;
		private CommandEventHandler _commandEventHandler;

		private GameStateEvents _gameStateServices;
		private SceneStateManager _sceneManager;

		private OptionPanel _optionPanel;

		private CommandManager _commandManager;

		private List<BuildingOption> _buildingOptions;
		private HexCoord _buildCoord;

		public PrototypeRulesSet(CommonServices commonServices, GameEngineServices gameEngineServices, OptionPanel optionPanel)
		{
			_commonServices = commonServices;
			_gameEngineServices = gameEngineServices;
			_gameStateServices = new GameStateEvents();
			_logicStateManager = new LogicStateManager(new LogicGameState());
			_sceneManager = new SceneStateManager(_commonServices, gameEngineServices, _gameStateServices);
			_commandManager = new CommandManager(_gameStateServices.LogicStateEvents);
			_commandEventHandler = new CommandEventHandler(_logicStateManager, _gameStateServices);
			_optionPanel = optionPanel;
		}


		public void Setup()
		{
			ResourceLoader.LoadResources(_commonServices);
			Builder.Build(_gameStateServices, _logicStateManager);
			_commonServices.RaycastConfig.SetRaycastLayer<HexTileObject>();
		}


		public void StartFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent += ColliderHit;
			_commandManager.NextCommandGroup();
		}


		public void StopFlow()
		{
			_commonServices.SceneEvents.ColliderSelectedEvent -= ColliderHit;

			_sceneManager.Dispose();
			_commandEventHandler.Dispose();
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
			if (hit.collider.GetComponent<HexTileObject>() is HexTileObject tile)
			{
				GameObject prefab = Resources.Load<GameObject>("Prefabs/PrototypeGame/UI/BuildingOption");
				_buildingOptions = new List<BuildingOption>();
				if (_logicStateManager.LogicGameState.Tiles[tile.HexCoord].Factory == null)
				{
					BuildingOption factoryOption = GameObject.Instantiate(prefab).GetComponent<BuildingOption>();
					factoryOption.Setup(Guid.NewGuid(), "Factory", true);
					_buildingOptions.Add(factoryOption);
				}
				if (_logicStateManager.LogicGameState.Tiles[tile.HexCoord].Station == null)
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
			}
		}

		private void HandleOptions(Guid guid)
		{
			_optionPanel.OptionSelectedEvent -= HandleOptions;
			_optionPanel.ClosePanel();
			BuildingOption option = _buildingOptions.FirstOrDefault(b => b.guid == guid);
			if (option != null)
			{
				switch (option.BuildingName)
				{
					case "Factory":
						_commandManager.CreateAndExecuteBuildFactoryCommand(_buildCoord, GoodsColor.GREEN);
						break;
					case "Station":
						_commandManager.CreateAndExecuteBuildStationCommand(_buildCoord);
						break;
				}
			}

		}
	}
}
