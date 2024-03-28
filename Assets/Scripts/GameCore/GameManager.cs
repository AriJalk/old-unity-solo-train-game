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

        [SerializeField]
        private InputManager inputManager;




        // Start is called before the first frame update
        void Awake()
        {
            ServiceLocator.SetPrefabManagerManager(prefabStorage);
            ServiceLocator.SetScriptableObjectManager();
            ServiceLocator.SetMaterialManager();
            gridController.Initialize();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
           
        }

    }

}