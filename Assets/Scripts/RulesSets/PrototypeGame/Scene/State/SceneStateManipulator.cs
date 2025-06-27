using CommonEngine.Core;
using CommonEngine.ResourceManagement;
using CommonEngine.SceneServices;
using PrototypeGame.Logic;
using UnityEngine;

namespace PrototypeGame.Scene.State
{
	/// <summary>
	/// Handles creation and manipulation of every game related scene objects, to be accessed only by SceneStateManager.
	/// *** Only Authority on modifying Scene Objects internals ***
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
			if (hexTileData.TerrainType == TerrainType.MOUNTAIN)
			{
				hexTileObject.MeshRenderer.material = _materialManager.Materials["RED"];
			}
			return hexTileObject;
		}


		public GoodsCubeObject BuildGoodsCubeObject(GoodsCube goodsCube)
		{
			GoodsCubeObject goodsCubeObject = _prefabManager.RetrievePoolObject<GoodsCubeObject>();
			goodsCubeObject.guid = goodsCube.guid;
			goodsCubeObject.MeshRenderer.material = _materialManager.Materials[goodsCube.Color.ToString()];
			return goodsCubeObject;
		}

		public void AttachGoodsCubeToSlot(GoodsCubeObject goodsCubeObject, GoodsCubeSlotObject goodsCubeSlotObject)
		{
			goodsCubeSlotObject.GoodsCubeObject = goodsCubeObject;
			SceneHelpers.SetParentAndResetPosition(goodsCubeObject.transform, goodsCubeSlotObject.GoodsCubeObjectContainer);
		}

		public void DetachGoodsCubeFromSlot(GoodsCubeSlotObject goodsCubeSlotObject)
		{
			GoodsCubeObject goodsCubeObject = goodsCubeSlotObject.GoodsCubeObject;
			goodsCubeSlotObject.GoodsCubeObject = null;
			_prefabManager.ReturnPoolObject(goodsCubeObject);
		}

		public FactoryObject BuildFactoryObject(Factory factory)
		{
			FactoryObject factoryObject = _prefabManager.RetrievePoolObject<FactoryObject>();
			return factoryObject;
		}

		public void AttachFactoryToTile(FactoryObject factoryObject, HexTileObject hexTileObject)
		{
			hexTileObject.FactoryObject = factoryObject;
			SceneHelpers.SetParentAndResetPosition
				(factoryObject.transform, hexTileObject.FactoryContainer);
		}

		public void DetachFactoryFromTile(HexTileObject hexTileObject)
		{
			_prefabManager.ReturnPoolObject(hexTileObject.FactoryObject);
			hexTileObject.FactoryObject = null;
		}

		public StationObject BuildStationObject(Station station)
		{
			StationObject stationObject = _prefabManager.RetrievePoolObject<StationObject>();
			return stationObject;
		}

		public void AttachStationToTile(StationObject stationObject, HexTileObject hexTileObject)
		{
			hexTileObject.StationObject = stationObject;
			SceneHelpers.SetParentAndResetPosition(stationObject.transform, hexTileObject.StationContainer);
		}

		public void DetachStationFromTile(HexTileObject hexTileObject)
		{
			_prefabManager.ReturnPoolObject(hexTileObject.StationObject);
			hexTileObject.StationObject = null;
		}


		public void TransportGoodsCube(GoodsCubeSlotObject origin, GoodsCubeSlotObject destination)
		{
			AttachGoodsCubeToSlot(origin.GoodsCubeObject, destination);
			origin.GoodsCubeObject = null;
		}

	}
}