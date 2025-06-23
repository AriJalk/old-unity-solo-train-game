using CardGame.Logic;
using CommonEngine.Core;
using CommonEngine.ResourceManagement;
using UnityEngine;

namespace CardGame.Scene
{
	internal class TileManipulator
	{
		private PrefabManager _prefabManager;
		public TileManipulator(CommonServices commonServices)
		{
			_prefabManager = commonServices.PrefabManager;
		}

		public void BuildCube(GoodsCubeSlotObject goodsCubeSlotObject, GoodsCube cubeData)
		{
			GoodsCubeObject cube = _prefabManager.RetrievePoolObject<GoodsCubeObject>();
			cube.guid = cubeData.guid;
			goodsCubeSlotObject.GoodsCubeObjectTransform = cube.transform;
			cube.transform.SetParent(goodsCubeSlotObject.transform);
			cube.transform.localPosition = Vector3.zero;
		}

		public void BuildFactory(HexTileObject tileObject, Factory factoryData)
		{
			FactoryObject factoryObject = _prefabManager.RetrievePoolObject<FactoryObject>();
			factoryObject.GoodsCubeSlotObject.guid = factoryData.GoodsCubeSlot.guid;
			factoryObject.transform.SetParent(tileObject.FactoryTransform);
			factoryObject.transform.localPosition = Vector3.zero;

			if (factoryData.GoodsCubeSlot.GoodsCube != null)
			{
				BuildCube(factoryObject.GoodsCubeSlotObject, factoryData.GoodsCubeSlot.GoodsCube);
			}
		}

		public HexTileObject BuildSceneTile(HexTileData tileData)
		{
			HexTileObject tileObject = _prefabManager.RetrievePoolObject<HexTileObject>();
			if (tileObject == null)
			{
				return null;
			}
			tileObject.HexCoord = tileData.HexCoord;
			if (tileData.Factory != null)
			{
				BuildFactory(tileObject, tileData.Factory);
			}

			return tileObject;
		}
	}
}