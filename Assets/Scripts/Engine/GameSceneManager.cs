using CardGame.Logic;
using CardGame.Scene;
using CommonEngine.Core;
using CommonEngine.IO;
using HexGridSystem;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameEngine.Core
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private HexGridController _gridController;
		[SerializeField]
		private CommonServiceLocator _serviceLocator;

		private SceneEvents _sceneEvents;
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{
			GameObject prefab = Resources.Load<GameObject>("Prefabs/HexTilePrefab");
			_serviceLocator.PrefabManager.RegisterPrefab<HexTileObject>(prefab);
			_sceneEvents = _serviceLocator.SceneEvents;
			_sceneEvents.ColliderSelectedEvent?.AddListener(ColliderHit);

			TileManipulator manipulator = new TileManipulator(_serviceLocator);
			HexTileData tile = new HexTileData();
			HexCoord coord = HexCoord.GetCoord(0, 0);
			tile.HexCoord = coord;
			_gridController.AddTileToGrid(manipulator.BuildSceneTile(tile));
			foreach (HexCoord neighbor in coord.GetNeighbors())
			{
				tile = new HexTileData();
				tile.HexCoord = neighbor;
				_gridController.AddTileToGrid(manipulator.BuildSceneTile(tile));
			}
		}

		private void OnDestroy()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		private void ColliderHit(RaycastHit hit)
		{
			if (hit.collider.GetComponent<HexTileObject>() is HexTileObject tile)
			{
				Debug.Log(tile.HexCoord);
			}
		}
	}
}