
using CardGame.Logic;
using System;

internal class GameStateEvents
{
    public event Action<HexTileData> TileBuiltEvent;
    public event Action<Guid, Guid> TransportCubeEvent;

    public void RaiseTileBuiltEvent(HexTileData data)
    {
        TileBuiltEvent?.Invoke(data);
    }

    public void RaiseTransportCubeEvent(Guid originSlot, Guid destinationSlot)
    {
        TransportCubeEvent?.Invoke(originSlot, destinationSlot);
    }
}
