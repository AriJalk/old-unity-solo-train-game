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
		public static void Build(GameStateEventsWrapper gameStateEventsWrapper, LogicMapStateManager logicMapStateManager, LogicCardStateManager logicCardStateManager, CardFactory cardFactory)
		{
			// Build test map
			HexCoord coord = HexCoord.GetCoord(0, 0);
			HexTileData tile = logicMapStateManager.BuildTile(coord, TerrainType.MOUNTAIN);

			gameStateEventsWrapper.SceneMapEvents.RaiseTileBuiltEvent(tile);

			foreach (HexCoord neighbor in coord.GetNeighbors())
			{
				tile = logicMapStateManager.BuildTile(neighbor, TerrainType.FIELD);

				gameStateEventsWrapper.SceneMapEvents.RaiseTileBuiltEvent(tile);
			}

			
			// Build test hand of cards
			for (int i = 0; i < 4; i++)
			{
				BuildActionCard card = cardFactory.CreateBasicBuildActionCard();
				logicCardStateManager.AddCardToHand(card);
				gameStateEventsWrapper.SceneCardEvents.RaiseCardCreatedAndAddedToHandEvent(card);
			}
		}
	}
}
