using Engine;
using UnityEngine;
using HexSystem;

namespace SoloTrainGame.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private HexGridController gridController;

        [SerializeField]
        private Transform prefabStorage;

        private InputManager _inputManager;



        // Start is called before the first frame update
        void Awake()
        {
            ServiceLocator.SetPrefabManagerManager(prefabStorage);
            gridController.Initialize();
            _inputManager = ServiceLocator.InputManager;
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

    }

}