using CommonEngine.Core;
using CommonEngine.ResourceManagement;
using CommonEngine.SceneServices;
using PrototypeGame.Logic;

namespace PrototypeGame.Scene.State
{
	/// <summary>
	/// Handles creation and manipulation of every game related scene objects, to be accessed only by SceneStateManager
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
			// TODO: move decisions to manager
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

		private GoodsCubeSlotObject InitializeGoodsCubeSlotObject(GoodsCubeSlotObject goodsCubeSlotObject, GoodsCubeSlot goodsCubeSlot)
		{
			goodsCubeSlotObject.guid = goodsCubeSlot.guid;

			_sceneGameState.CubeSlots.Add(goodsCubeSlot.guid, goodsCubeSlotObject);

			if(goodsCubeSlot.GoodsCube != null)
			{
				BuildGoodsCubeOnSlot(goodsCubeSlotObject, goodsCubeSlot.GoodsCube);
			}

			return goodsCubeSlotObject;
		}

		private void RemoveGoodsCubeSlot(GoodsCubeSlotObject goodsCubeSlotObject)
		{
			// Remove cube if exists
			if (goodsCubeSlotObject.GoodsCubeObject != null)
			{
				RemoveGoodsCubeObjectFromSlot(goodsCubeSlotObject);
			}
			_sceneGameState.CubeSlots.Remove(goodsCubeSlotObject.guid);
			//Debug.Log("Slots: " + _sceneGameState.CubeSlots.Count);
		}

		private GoodsCubeObject BuildGoodsCube(GoodsCube goodsCube)
		{
			GoodsCubeObject goodsCubeObject = _prefabManager.RetrievePoolObject<GoodsCubeObject>();
			goodsCubeObject.guid = goodsCube.guid;
			goodsCubeObject.MeshRenderer.material = _materialManager.Materials[goodsCube.Color.ToString()];

			_sceneGameState.Cubes.Add(goodsCube.guid, goodsCubeObject);
			return goodsCubeObject;
		}

		public GoodsCubeSlotObject BuildGoodsCubeOnSlot(GoodsCubeSlotObject goodsCubeSlotObject, GoodsCube goodsCube)
		{
			GoodsCubeObject goodsCubeObject = BuildGoodsCube(goodsCube);
			SceneHelpers.SetParentAndResetPosition(goodsCubeObject.transform, goodsCubeSlotObject.GoodsCubeObjectContainer);
			goodsCubeSlotObject.GoodsCubeObject = goodsCubeObject;

			return goodsCubeSlotObject;
		}

		public void RemoveGoodsCubeObjectFromSlot(GoodsCubeSlotObject goodsCubeSlotObject)
		{
			GoodsCubeObject goodsCubeObject = goodsCubeSlotObject.GoodsCubeObject;
			_sceneGameState.Cubes.Remove(goodsCubeObject.guid);
			_prefabManager.ReturnPoolObject(goodsCubeObject);
			goodsCubeSlotObject.GoodsCubeObject = null;
		}

		public FactoryObject BuildFactoryOnTile(HexTileObject hexTileObject, Factory factory)
		{
			FactoryObject factoryObject = _prefabManager.RetrievePoolObject<FactoryObject>();

			InitializeGoodsCubeSlotObject(factoryObject.GoodsCubeSlotObject, factory.GoodsCubeSlot);

			SceneHelpers.SetParentAndResetPosition(factoryObject.transform, hexTileObject.FactoryContainer);

			hexTileObject.FactoryObject = factoryObject;

			return factoryObject;
		}

		public void RemoveFactoryFromTile(HexTileObject hexTileObject)
		{
			RemoveGoodsCubeSlot(hexTileObject.FactoryObject.GoodsCubeSlotObject);
			_prefabManager.ReturnPoolObject(hexTileObject.FactoryObject);
			hexTileObject.FactoryObject = null;
		}

		public StationObject BuildStationOnTile(HexTileObject hexTileObject, Station station)
		{
			StationObject stationObject = _prefabManager.RetrievePoolObject<StationObject>();
			InitializeGoodsCubeSlotObject(stationObject.GoodsCubeSlotObject1, station.GoodsCubeSlot1);
			InitializeGoodsCubeSlotObject(stationObject.GoodsCubeSlotObject2, station.GoodsCubeSlot2);

			SceneHelpers.SetParentAndResetPosition(stationObject.transform, hexTileObject.StationContainer);

			hexTileObject.StationObject = stationObject;

			return stationObject;
		}

		public void RemoveStationFromTile(HexTileObject hexTileObject)
		{
			RemoveGoodsCubeSlot(hexTileObject.StationObject.GoodsCubeSlotObject1);
			RemoveGoodsCubeSlot(hexTileObject.StationObject.GoodsCubeSlotObject2);
			_prefabManager.ReturnPoolObject(hexTileObject.StationObject);
			hexTileObject.StationObject = null;
		}

		public void TransportGoodsCube(GoodsCubeSlotObject origin, GoodsCubeSlotObject destination)
		{
			SceneHelpers.SetParentAndResetPosition(origin.GoodsCubeObjectContainer, destination.transform);
			destination.GoodsCubeObjectContainer = origin.GoodsCubeObjectContainer;
			origin.GoodsCubeObjectContainer = null;
		}

	}
}