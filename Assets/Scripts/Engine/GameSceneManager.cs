using CommonEngine.IO;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameEngine.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private SceneEvents _sceneEvents;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _sceneEvents.ColliderSelectedEvent?.AddListener(ColliderHit);
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
           if (hit.collider.GetComponent<HexTile>() is HexTile tile)
            {
                Debug.Log(tile.HexCoord);
            }
        }
    }
}