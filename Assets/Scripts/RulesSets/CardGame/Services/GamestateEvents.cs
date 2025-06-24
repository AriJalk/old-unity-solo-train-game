
using CardGame.Logic;
using System;

internal class GameStateEvents
{
    public event Action<HexTileData> TileBuiltEvent;

    public void RaiseTileBuiltEvent(HexTileData data)
    {
        TileBuiltEvent?.Invoke(data);
    }
}
