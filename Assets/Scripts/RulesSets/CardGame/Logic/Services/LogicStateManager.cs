using CardGame.Logic.MetaData;
using HexSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Logic.Services
{
	/// <summary>
	/// Main API for interacting with the logic state
	/// </summary>
	internal class LogicStateManager
	{
		private LogicGameState _logicGameState;

		public LogicStateManager(LogicGameState logicGameState)
		{
			_logicGameState = logicGameState;
		}

		public HexTileData BuildTile(HexCoord coordinates, TerrainType terrainType)
		{
			if (_logicGameState.Tiles.ContainsKey(coordinates))
			{
				return null;
			}
			HexTileData hexTileData = new HexTileData(coordinates, terrainType);
			_logicGameState.Tiles.Add(coordinates, hexTileData);
			_logicGameState.TileToSlots.Add(hexTileData, new List<GoodsCubeSlot>());
			return hexTileData;
		}

		public Factory BuildFactoryOnTile(HexTileData hexTileData, GoodsColor productionColor)
		{
			Factory factory = new Factory(Guid.NewGuid(), productionColor);
			_logicGameState.Factories.Add(factory.guid, factory);
			GoodsCubeSlot slot = new GoodsCubeSlot(Guid.NewGuid());
			factory.GoodsCubeSlot = slot;
			SlotInfo slotInfo = new SlotInfo() { 
				CanPlace = false,
				HexTileData = hexTileData,
				ParentEntity = factory,
				Slot = slot, 
				Type = typeof(Factory) };
			_logicGameState.CubeSlotInfo.Add(slot.guid, slotInfo);

			hexTileData.Factory = factory;
			return factory;
		}

		public Station BuildStationOnTile(HexTileData hexTileData, bool isUpgraded = false)
		{
			Station station = new Station(Guid.NewGuid(), isUpgraded);
			_logicGameState.Stations.Add(station.guid, station);
			GoodsCubeSlot slot1 = new GoodsCubeSlot(Guid.NewGuid());
			GoodsCubeSlot slot2 = new GoodsCubeSlot(Guid.NewGuid());

			SlotInfo slotInfo1 = SlotInfo.CreateSlotInfo(slot1, hexTileData, typeof(Station), true, station);
			SlotInfo slotInfo2 = SlotInfo.CreateSlotInfo(slot2, hexTileData, typeof(Station), isUpgraded, station);
			_logicGameState.CubeSlotInfo.Add(slot1.guid, slotInfo1);
			_logicGameState.CubeSlotInfo.Add(slot2.guid, slotInfo2);

			station.GoodsCubeSlot1 = slot1;
			station.GoodsCubeSlot2 = slot2;
			hexTileData.Station = station;
			return station;
		}

		public GoodsCube ProduceCubeInFactory(Factory factory)
		{
			GoodsCube cube = new GoodsCube(Guid.NewGuid(), factory.ProductionColor);
			factory.GoodsCubeSlot.GoodsCube = cube;
			_logicGameState.CubeToSlot[cube.guid] = factory.GoodsCubeSlot;

			return cube;
		}

		public void TransportGoodsCube(GoodsCubeSlot origin, GoodsCubeSlot destination)
		{
			GoodsCube cube = origin.GoodsCube;
			origin.GoodsCube = null;

			destination.GoodsCube = cube;

			_logicGameState.CubeToSlot[cube.guid] = destination;
		}
	}
}
