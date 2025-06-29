using PrototypeGame.Logic.MetaData;
using HexSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame.Logic.State
{
	/// <summary>
	/// Main API for interacting with the logic state, only authority to modify LogicMapState
	/// </summary>
	internal class LogicMapStateManager
	{
		public readonly LogicMapState LogicMapState;

		public LogicMapStateManager(LogicMapState logicMapState)
		{
			LogicMapState = logicMapState;
		}

		public HexTileData BuildTile(HexCoord coordinates, TerrainType terrainType)
		{
			if (LogicMapState.Tiles.ContainsKey(coordinates))
			{
				return null;
			}
			HexTileData hexTileData = new HexTileData(coordinates, terrainType);
			LogicMapState.Tiles.Add(coordinates, hexTileData);
			LogicMapState.TileToSlots.Add(hexTileData, new List<GoodsCubeSlot>());
			return hexTileData;
		}

		public Factory BuildFactoryOnTile(HexTileData hexTileData, GoodsColor productionColor)
		{
			Factory factory = new Factory(Guid.NewGuid(), productionColor);
			LogicMapState.Factories.Add(factory.guid, factory);
			GoodsCubeSlot slot = new GoodsCubeSlot(Guid.NewGuid());
			factory.GoodsCubeSlot = slot;
			SlotInfo slotInfo = new SlotInfo() { 
				CanPlace = false,
				HexTileData = hexTileData,
				ParentEntity = factory,
				Slot = slot, 
				Type = typeof(Factory) };
			LogicMapState.CubeSlotInfo.Add(slot.guid, slotInfo);
			LogicMapState.TileToSlots[hexTileData].Add(slot);

			hexTileData.Factory = factory;
			return factory;
		}

		public Station BuildStationOnTile(HexTileData hexTileData, bool isUpgraded = false)
		{
			Station station = new Station(Guid.NewGuid(), isUpgraded);
			LogicMapState.Stations.Add(station.guid, station);
			GoodsCubeSlot slot1 = new GoodsCubeSlot(Guid.NewGuid());
			GoodsCubeSlot slot2 = new GoodsCubeSlot(Guid.NewGuid());

			SlotInfo slotInfo1 = SlotInfo.CreateSlotInfo(slot1, hexTileData, typeof(Station), true, station);
			SlotInfo slotInfo2 = SlotInfo.CreateSlotInfo(slot2, hexTileData, typeof(Station), isUpgraded, station);
			LogicMapState.CubeSlotInfo.Add(slot1.guid, slotInfo1);
			LogicMapState.CubeSlotInfo.Add(slot2.guid, slotInfo2);

			station.GoodsCubeSlot1 = slot1;
			station.GoodsCubeSlot2 = slot2;
			hexTileData.Station = station;
			return station;
		}

		public GoodsCube ProduceGoodsCubeInSlot(GoodsCubeSlot goodsCubeSlot, GoodsColor goodsColor)
		{
			GoodsCube cube = new GoodsCube(Guid.NewGuid(), goodsColor);
			goodsCubeSlot.GoodsCube = cube;
			LogicMapState.CubeToSlot[cube.guid] = goodsCubeSlot;

			return cube;
		}

		public void TransportGoodsCube(GoodsCubeSlot origin, GoodsCubeSlot destination)
		{
			GoodsCube cube = origin.GoodsCube;
			origin.GoodsCube = null;

			destination.GoodsCube = cube;

			LogicMapState.CubeToSlot[cube.guid] = destination;
		}

		public void RemoveCube(GoodsCube goodsCube)
		{
			GoodsCubeSlot goodsCubeSlot = LogicMapState.CubeToSlot[goodsCube.guid];
			goodsCubeSlot.GoodsCube = null;
			LogicMapState.CubeToSlot.Remove(goodsCube.guid);

			//Debug.Log("Logic cubes: " + LogicMapState.CubeToSlot.Count);
		}

		public void RemoveSlot(GoodsCubeSlot goodsCubeSlot)
		{
			if (goodsCubeSlot.GoodsCube != null)
			{
				RemoveCube(goodsCubeSlot.GoodsCube);
			}
			SlotInfo slotInfo = LogicMapState.CubeSlotInfo[goodsCubeSlot.guid];
			LogicMapState.CubeSlotInfo.Remove(goodsCubeSlot.guid);
			LogicMapState.TileToSlots[slotInfo.HexTileData].Remove(goodsCubeSlot);
			//Debug.Log(LogicMapState.CubeSlotInfo.Count);
		}

		public void RemoveFactory(HexTileData hexTileData)
		{
			Factory factory = hexTileData.Factory;
			LogicMapState.Factories.Remove(factory.guid);
			LogicMapState.TileToSlots[hexTileData].Remove(factory.GoodsCubeSlot);
			RemoveSlot(factory.GoodsCubeSlot);

			hexTileData.Factory = null;
		}

		public void RemoveStation(HexTileData hexTileData)
		{
			Station station = hexTileData.Station;
			LogicMapState.Stations.Remove(station.guid);
			RemoveSlot(station.GoodsCubeSlot1);
			RemoveSlot(station.GoodsCubeSlot2);
			hexTileData.Station = null;
		}
	}
}
