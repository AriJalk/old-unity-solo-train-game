using UnityEngine.Events;

namespace SoloTrainGame.Core
{
    public class GameEvents
    {
        public UnityEvent<HexTileObject> TileSelectedEvent;

        public GameEvents()
        {
            TileSelectedEvent = new UnityEvent<HexTileObject>();
        }

    }
}