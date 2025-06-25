using PrototypeGame.Scene;
using CommonEngine.Core;
using UnityEngine;

namespace PrototypeGame.GameBuilder
{
	internal class ResourceLoader
	{
		public static void LoadResources(CommonServices _commonServices)
		{
			_commonServices.PrefabManager.LoadAndRegisterPrefab<HexTileObject>("PrototypeGame/HexTilePrefab");
			_commonServices.PrefabManager.LoadAndRegisterPrefab<FactoryObject>("PrototypeGame/FactoryPrefab");
			_commonServices.PrefabManager.LoadAndRegisterPrefab<GoodsCubeObject>("PrototypeGame/GoodsCubePrefab");
			_commonServices.PrefabManager.LoadAndRegisterPrefab<StationObject>("PrototypeGame/StationPrefab");
			_commonServices.MaterialManager.Materials.Add("GREEN", Resources.Load<Material>("Materials/GreenMaterial"));
			_commonServices.MaterialManager.Materials.Add("RED", Resources.Load<Material>("Materials/RedMaterial"));
			_commonServices.MaterialManager.Materials.Add("BLUE", Resources.Load<Material>("Materials/BlueMaterial"));
		}
	}
}
