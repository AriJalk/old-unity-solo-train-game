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




        // Start is called before the first frame update
        void Awake()
        {
            ServiceLocator.SetPrefabManagerManager(prefabStorage);
            ServiceLocator.SetScriptableObjectManager();
            gridController.Initialize();
        }

        void Start()
        {
            gridController.CreateTile(HexPosition.ZERO);

        }

        // Update is called once per frame
        void Update()
        {
           
        }

    }

}