using CardGame.Logic;
using CommonEngine.Core;
using CommonEngine.ResourceManagement;
using CommonEngine.SceneServices;
using GameEngine.Core;
using GameEngine.Map;

namespace CardGame.Scene.Services
{
	internal class SceneStateManipulator
	{
		private PrefabManager _prefabManager;

		public SceneStateManipulator(CommonServices commonServices)
		{
			_prefabManager = commonServices.PrefabManager;
		}


		public HexTileObject BuildTile(HexTileData hexTileData)
		{
			HexTileObject hexTileObject = _prefabManager.RetrievePoolObject<HexTileObject>();
			hexTileObject.HexCoord = hexTileData.HexCoord;
			if (hexTileData.Factory != null)
			{
				BuildFactoryOnTile(hexTileObject, hexTileData.Factory);
			}

			return hexTileObject;
		}

		public HexTileObject BuildFactoryOnTile(HexTileObject hexTileObject, Factory factory)
		{
			FactoryObject factoryObject = _prefabManager.RetrievePoolObject<FactoryObject>();
			factoryObject.GoodsCubeSlotObject.guid = factory.GoodsCubeSlot.guid;
			SceneHelpers.SetParentAndResetPosition(factoryObject.transform, hexTileObject.FactoryTransform);
			if (factory.GoodsCubeSlot.GoodsCube != null)
			{
				BuildGoodsCubeOnSlot(factoryObject.GoodsCubeSlotObject, factory.GoodsCubeSlot.GoodsCube);
			}
			return hexTileObject;
		}

		public GoodsCubeObject BuildGoodsCubeOnSlot(GoodsCubeSlotObject slot, GoodsCube cube)
		{
			GoodsCubeObject goodsCubeObject = _prefabManager.RetrievePoolObject<GoodsCubeObject>();
			goodsCubeObject.guid = cube.guid;
			SceneHelpers.SetParentAndResetPosition(goodsCubeObject.transform, slot.transform);
			return goodsCubeObject;
		}

		public static void TransportGoodsCube(GoodsCubeSlotObject origin, GoodsCubeSlotObject destination)
		{
			SceneHelpers.SetParentAndResetPosition(origin.GoodsCubeObjectTransform, destination.transform);
			destination.GoodsCubeObjectTransform = origin.GoodsCubeObjectTransform;
			origin.GoodsCubeObjectTransform = null;
		}

	}
}