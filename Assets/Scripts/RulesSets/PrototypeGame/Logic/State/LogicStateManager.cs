using PrototypeGame.Logic.MetaData;
using HexSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame.Logic.State
{
	/// <summary>
	/// Main API for interacting with the logic state, only authority to modify LogicGameState
	/// </summary>
	internal class LogicStateManager
	{
		public LogicGameState LogicGameState;

		public LogicStateManager(LogicGameState logicGameState)
		{
			LogicGameState = logicGameState;
		}

		public HexTileData BuildTile(HexCoord coordinates, TerrainType terrainType)
		{
			if (LogicGameState.Tiles.ContainsKey(coordinates))
			{
				return null;
			}
			HexTileData hexTileData = new HexTileData(coordinates, terrainType);
			LogicGameState.Tiles.Add(coordinates, hexTileData);
			LogicGameState.TileToSlots.Add(hexTileData, new List<GoodsCubeSlot>());
			return hexTileData;
		}

		public Factory BuildFactoryOnTile(HexTileData hexTileData, GoodsColor productionColor)
		{
			Factory factory = new Factory(Guid.NewGuid(), productionColor);
			LogicGameState.Factories.Add(factory.guid, factory);
			GoodsCubeSlot slot = new GoodsCubeSlot(Guid.NewGuid());
			factory.GoodsCubeSlot = slot;
			SlotInfo slotInfo = new SlotInfo() { 
				CanPlace = false,
				HexTileData = hexTileData,
				ParentEntity = factory,
				Slot = slot, 
				Type = typeof(Factory) };
			LogicGameState.CubeSlotInfo.Add(slot.guid, slotInfo);

			hexTileData.Factory = factory;
			return factory;
		}

		public Station BuildStationOnTile(HexTileData hexTileData, bool isUpgraded = false)
		{
			Station station = new Station(Guid.NewGuid(), isUpgraded);
			LogicGameState.Stations.Add(station.guid, station);
			GoodsCubeSlot slot1 = new GoodsCubeSlot(Guid.NewGuid());
			GoodsCubeSlot slot2 = new GoodsCubeSlot(Guid.NewGuid());

			SlotInfo slotInfo1 = SlotInfo.CreateSlotInfo(slot1, hexTileData, typeof(Station), true, station);
			SlotInfo slotInfo2 = SlotInfo.CreateSlotInfo(slot2, hexTileData, typeof(Station), isUpgraded, station);
			LogicGameState.CubeSlotInfo.Add(slot1.guid, slotInfo1);
			LogicGameState.CubeSlotInfo.Add(slot2.guid, slotInfo2);

			station.GoodsCubeSlot1 = slot1;
			station.GoodsCubeSlot2 = slot2;
			hexTileData.Station = station;
			return station;
		}

		public GoodsCube ProduceCubeInFactory(Factory factory)
		{
			GoodsCube cube = new GoodsCube(Guid.NewGuid(), factory.ProductionColor);
			factory.GoodsCubeSlot.GoodsCube = cube;
			LogicGameState.CubeToSlot[cube.guid] = factory.GoodsCubeSlot;

			return cube;
		}

		public void TransportGoodsCube(GoodsCubeSlot origin, GoodsCubeSlot destination)
		{
			GoodsCube cube = origin.GoodsCube;
			origin.GoodsCube = null;

			destination.GoodsCube = cube;

			LogicGameState.CubeToSlot[cube.guid] = destination;
		}

		public void RemoveCube(GoodsCube goodsCube)
		{
			LogicGameState.CubeToSlot.Remove(goodsCube.guid);
			//Debug.Log("Logic cubes: " + LogicGameState.CubeToSlot.Count);
		}

		public void RemoveSlot(GoodsCubeSlot goodsCubeSlot)
		{
			if (goodsCubeSlot.GoodsCube != null)
			{
				RemoveCube(goodsCubeSlot.GoodsCube);
			}
			SlotInfo slotInfo = LogicGameState.CubeSlotInfo[goodsCubeSlot.guid];
			LogicGameState.CubeSlotInfo.Remove(goodsCubeSlot.guid);
			LogicGameState.TileToSlots[slotInfo.HexTileData].Remove(goodsCubeSlot);
			//Debug.Log(LogicGameState.CubeSlotInfo.Count);
		}

		public void RemoveFactory(HexTileData hexTileData)
		{
			Factory factory = hexTileData.Factory;
			LogicGameState.Factories.Remove(factory.guid);
			RemoveSlot(factory.GoodsCubeSlot);

			hexTileData.Factory = null;
		}

		public void RemoveStation(HexTileData hexTileData)
		{
			Station station = hexTileData.Station;
			LogicGameState.Stations.Remove(station.guid);
			RemoveSlot(station.GoodsCubeSlot1);
			RemoveSlot(station.GoodsCubeSlot2);
			hexTileData.Station = null;
		}
	}
}
