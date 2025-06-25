using CardGame.Scene;
using CommonEngine.Core;
using UnityEngine;

namespace CardGame.GameBuilder
{
	internal class ResourceLoader
	{
		public static void LoadResources(CommonServices _commonServices)
		{
			_commonServices.PrefabManager.LoadAndRegisterPrefab<HexTileObject>("CardGame/HexTilePrefab");
			_commonServices.PrefabManager.LoadAndRegisterPrefab<FactoryObject>("CardGame/FactoryPrefab");
			_commonServices.PrefabManager.LoadAndRegisterPrefab<GoodsCubeObject>("CardGame/GoodsCubePrefab");
			_commonServices.PrefabManager.LoadAndRegisterPrefab<StationObject>("CardGame/StationPrefab");
			_commonServices.MaterialManager.Materials.Add("GREEN", Resources.Load<Material>("Materials/GreenMaterial"));
			_commonServices.MaterialManager.Materials.Add("RED", Resources.Load<Material>("Materials/RedMaterial"));
			_commonServices.MaterialManager.Materials.Add("BLUE", Resources.Load<Material>("Materials/BlueMaterial"));
		}
	}
}
