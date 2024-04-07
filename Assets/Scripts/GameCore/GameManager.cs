using Engine;
using UnityEngine;
using HexSystem;
using System.Collections.Generic;
using SoloTrainGame.GameLogic;

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
        [SerializeField]
        private GraphicUserInterface _userInterface;

        private InputManager _inputManager;

        private Stack<Turn> _turnStack;
        static public GameState GameState;



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
            _turnStack = new Stack<Turn>();
            GameState = new GameState(_gridController);
        }

        void Start()
        {
            List<CardInstance> cards = new List<CardInstance>();
            foreach (CardSO card in ServiceLocator.ScriptableObjectManager.CardTypes)
            {
                CardInstance cardInstance = new CardInstance(card);
                GameState.CardHand.Add(cardInstance);
                _userInterface.Hand.AddCardToHandFromInstance(cardInstance);
                cards.Add(cardInstance);
            }
            _userInterface.CardGridViewer.OpenViewer(cards);
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