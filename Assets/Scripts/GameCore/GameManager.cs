using Engine;
using UnityEngine;
using System.Collections.Generic;
using SoloTrainGame.GameLogic;
using SoloTrainGame.UI;
using System;

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
        private IActionState _actionState;
        private StateManager _stateManager;

        private LogicState GameState;

        private HexTileObject _selectedTile;



        // Start is called before the first frame update
        void Awake()
        {
            Initialize();
        }

        void Start()
        {
            TestHand();
            //_userInterface.CardGridViewer.OpenViewer(cards);
            _rotatedCamera.ColliderClickDownEvent?.AddListener(RaycastColliderHitDown);
            _rotatedCamera.ColliderClickUpEvent?.AddListener(RaycastColliderHitUp);
        }



        // Update is called once per frame
        void Update()
        {
            _inputManager.UpdateInput();
            ServiceLocator.TimerManager.Update();
        }

        private void OnDestroy()
        {
            _stateManager.ExitCurrentState();
            _rotatedCamera.ColliderClickDownEvent?.RemoveListener(RaycastColliderHitDown);
            _rotatedCamera.ColliderClickUpEvent?.RemoveListener(RaycastColliderHitUp);
        }

        private void Initialize()
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
            GameState = new LogicState(_gridController);
            ServiceLocator.SetUserInterface(_userInterface);
            _stateManager = new StateManager();
            _stateManager.AddState(new ChooseActionCardState());
            _stateManager.EnterNextState();
        }

        private void RaycastColliderHitDown(RaycastHit hit)
        {
            if (hit.collider.transform.parent?.GetComponent<HexTileObject>() is HexTileObject tile)
            {
                _selectedTile = tile;
            }
        }
        private void RaycastColliderHitUp(RaycastHit hit)
        {
            if (hit.collider.transform.parent?.GetComponent<HexTileObject>() is HexTileObject tile)
            {
                if (tile == _selectedTile)
                {
                    Debug.Log(tile.HexGameData);
                }
            }
        }


        private void TestHand()
        {
            List<CardInstance> cards = new List<CardInstance>();
            for (int i = 0; i < 1; i++)
            {
                foreach (CardSO card in ServiceLocator.ScriptableObjectManager.CardTypes)
                {
                    CardInstance cardInstance = new CardInstance(card);
                    GameState.CardHand.Add(cardInstance);
                    _userInterface.Hand.AddCardToHandFromInstance(cardInstance);
                    cards.Add(cardInstance);

                }
            }
        }
    }

}