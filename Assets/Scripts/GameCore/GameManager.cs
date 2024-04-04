using Engine;
using UnityEngine;
using HexSystem;

namespace SoloTrainGame.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private HexGridController _gridController;
        [SerializeField]
        private Transform _prefabStorage;

        [SerializeField]
        private RotatedCamera _rotatedCamera;
        [SerializeField]
        private Transform _centerObject;

        private InputManager _inputManager;



        // Start is called before the first frame update
        void Awake()
        {
            ServiceLocator.SetPrefabManagerManager(_prefabStorage); 
            _inputManager = ServiceLocator.InputManager;
            Vector2 min = transform.position;
            Vector2 max = transform.position;
            if (_gridController != null)
            {
                _gridController.Initialize();
                min = new Vector2(_gridController.MinX, _gridController.MinZ);
                max = new Vector2(_gridController.MaxX, _gridController.MaxZ);
                
            }
            else if (_centerObject != null)
            {
                min = new Vector2(_centerObject.position.x, _centerObject.position.z);
                max = min;
            }
            _rotatedCamera.Initialize(min, max);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _inputManager.UpdateInput();
            ServiceLocator.TimerManager.Update();
        }

        HexTileObject RaycastHitToHexTile(RaycastHit hit)
        {
            if (hit.collider != null && hit.collider.transform.parent?.GetComponent<HexTileObject>() is HexTileObject tileObject)
            {
                return tileObject;
            }
            return null;
        }

    }

}