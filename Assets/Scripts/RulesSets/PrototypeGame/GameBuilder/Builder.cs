using PrototypeGame.Logic;
using PrototypeGame.Logic.State;
using PrototypeGame.Events;
using HexSystem;
using PrototypeGame.Logic.State.Cards;
using PrototypeGame.Logic.Components.Cards;
using System;

namespace PrototypeGame.GameBuilder
{
	internal class Builder
	{
		public static void Build(GameStateEvents gameStateEvents, LogicMapStateManager logicMapStateManager, LogicCardStateManager logicCardStateManager)
		{
			HexCoord coord = HexCoord.GetCoord(0, 0);
			HexTileData tile = logicMapStateManager.BuildTile(coord, TerrainType.MOUNTAIN);

			gameStateEvents.SceneMapEvents.RaiseTileBuiltEvent(tile);

			foreach (HexCoord neighbor in coord.GetNeighbors())
			{
				tile = logicMapStateManager.BuildTile(neighbor, TerrainType.FIELD);

				gameStateEvents.SceneMapEvents.RaiseTileBuiltEvent(tile);
			}

			ProtoCardData card1 = new ProtoCardData(Guid.NewGuid(), "Test card", 2, 2, "Build test card");
			logicCardStateManager.AddCardToHand(card1);
			gameStateEvents.SceneCardEvents.RaiseCardCreatedAndAddedToHandEvent(card1);
		}
	}
}
