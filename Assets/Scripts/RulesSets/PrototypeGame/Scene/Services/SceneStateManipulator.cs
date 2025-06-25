using PrototypeGame.Logic;
using CommonEngine.Core;
using CommonEngine.ResourceManagement;
using CommonEngine.SceneServices;
using UnityEngine.Tilemaps;

namespace PrototypeGame.Scene.Services
{
	/// <summary>
	/// Handles creation and manipulation of every game related scene objects, to be accessed only by SceneManager
	/// </summary>
	internal class SceneStateManipulator
	{
		private PrefabManager _prefabManager;
		private MaterialManager _materialManager;
		private SceneGameState _sceneGameState;

		public SceneStateManipulator(CommonServices commonServices, SceneGameState sceneGameState)
		{
			_prefabManager = commonServices.PrefabManager;
			_materialManager = commonServices.MaterialManager;
			_sceneGameState = sceneGameState;
		}


		public HexTileObject BuildTile(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _prefabManager.RetrievePoolObject<HexTileObject>();
			hexTileObject.HexCoord = hexTileData.HexCoord;
			if (hexTileData.Factory != null)
			{
				BuildFactoryOnTile(hexTileObject, hexTileData.Factory);
			}
			if (hexTileData.Station != null)
			{
				BuildStationOnTile(hexTileObject, hexTileData.Station);
			}

			_sceneGameState.Tiles.Add(hexTileData.HexCoord, hexTileObject);

			if (hexTileData.TerrainType == TerrainType.MOUNTAIN)
			{
				hexTileObject.MeshRenderer.material = _materialManager.Materials["RED"];
			}
			return hexTileObject;
		}

		public StationObject BuildStationOnTile(HexTileObject hexTileObject, Station station)
		{
			StationObject stationObject = _prefabManager.RetrievePoolObject<StationObject>();
			stationObject.GoodsCubeSlotObject1.guid = station.GoodsCubeSlot1.guid;
			stationObject.GoodsCubeSlotObject2.guid = station.GoodsCubeSlot2.guid;

			_sceneGameState.CubeSlots.Add(stationObject.GoodsCubeSlotObject1.guid, stationObject.GoodsCubeSlotObject1);
			_sceneGameState.CubeSlots.Add(stationObject.GoodsCubeSlotObject2.guid, stationObject.GoodsCubeSlotObject2);

			SceneHelpers.SetParentAndResetPosition(stationObject.transform, hexTileObject.StationTransform);

			if (station.GoodsCubeSlot1.GoodsCube != null)
			{
				BuildGoodsCubeOnSlot(stationObject.GoodsCubeSlotObject1, station.GoodsCubeSlot1.GoodsCube);
			}
			if (station.GoodsCubeSlot2.GoodsCube != null)
			{
				BuildGoodsCubeOnSlot(stationObject.GoodsCubeSlotObject2, station.GoodsCubeSlot2.GoodsCube);
			}

			return stationObject;
		}

		public FactoryObject BuildFactoryOnTile(HexTileObject hexTileObject, Factory factory)
		{
			FactoryObject factoryObject = _prefabManager.RetrievePoolObject<FactoryObject>();
			factoryObject.GoodsCubeSlotObject.guid = factory.GoodsCubeSlot.guid;
			_sceneGameState.CubeSlots.Add(factory.GoodsCubeSlot.guid, factoryObject.GoodsCubeSlotObject);

			SceneHelpers.SetParentAndResetPosition(factoryObject.transform, hexTileObject.FactoryTransform);
			if (factory.GoodsCubeSlot.GoodsCube != null)
			{
				BuildGoodsCubeOnSlot(factoryObject.GoodsCubeSlotObject, factory.GoodsCubeSlot.GoodsCube);
			}

			return factoryObject;
		}

		public GoodsCubeObject BuildGoodsCubeOnSlot(GoodsCubeSlotObject slot, GoodsCube cube)
		{
			GoodsCubeObject goodsCubeObject = _prefabManager.RetrievePoolObject<GoodsCubeObject>();
			goodsCubeObject.guid = cube.guid;
			SceneHelpers.SetParentAndResetPosition(goodsCubeObject.transform, slot.transform);
			slot.GoodsCubeObjectTransform = goodsCubeObject.transform;

			_sceneGameState.Cubes.Add(goodsCubeObject.guid, goodsCubeObject);

			goodsCubeObject.MeshRenderer.material = _materialManager.Materials[cube.Color.ToString()];
			return goodsCubeObject;
		}

		public void TransportGoodsCube(GoodsCubeSlotObject origin, GoodsCubeSlotObject destination)
		{
			SceneHelpers.SetParentAndResetPosition(origin.GoodsCubeObjectTransform, destination.transform);
			destination.GoodsCubeObjectTransform = origin.GoodsCubeObjectTransform;
			origin.GoodsCubeObjectTransform = null;
		}

	}
}