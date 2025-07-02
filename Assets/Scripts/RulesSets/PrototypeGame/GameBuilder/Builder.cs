using PrototypeGame.Logic;
using PrototypeGame.Logic.State;
using PrototypeGame.Events;
using HexSystem;
using PrototypeGame.Logic.State.Cards;
using PrototypeGame.Logic.Components.Cards;
using System;
using PrototypeGame.Scene.Components.Cards;

namespace PrototypeGame.GameBuilder
{
	internal class Builder
	{
		public static void Build(SceneEventsWrapper sceneEventsWrapper, LogicMapStateManager logicMapStateManager, LogicCardStateManager logicCardStateManager, CardFactory cardFactory)
		{
			// Build test map
			HexCoord coord = HexCoord.GetCoord(0, 0);
			HexTileData tile = logicMapStateManager.BuildTile(coord, TerrainType.MOUNTAIN);

			sceneEventsWrapper.SceneMapEvents.RaiseTileBuiltEvent(tile);

			logicMapStateManager.BuildStationOnTile(tile);
			sceneEventsWrapper.SceneMapEvents.RaiseStationBuiltEvent(tile);


			foreach (HexCoord neighbor in coord.GetNeighbors())
			{
				tile = logicMapStateManager.BuildTile(neighbor, TerrainType.FIELD);

				sceneEventsWrapper.SceneMapEvents.RaiseTileBuiltEvent(tile);

				//logicMapStateManager.BuildFactoryOnTile(tile, GoodsColor.GREEN);
				//sceneEventsWrapper.SceneMapEvents.RaiseFactoryBuiltEvent(tile);

				//logicMapStateManager.ProduceGoodsCubeInSlot(tile.Factory.GoodsCubeSlot, tile.Factory.ProductionColor);
				//sceneEventsWrapper.SceneMapEvents.RaiseGoodsCubeProducedInSlotEvent(tile.Factory.GoodsCubeSlot, tile.Factory.GoodsCubeSlot.GoodsCube);
			}

			
			// Build test hand of cards
			for (int i = 0; i < 2; i++)
			{
				ProtoCardData card = cardFactory.CreateBasicBuildActionCard();
				logicCardStateManager.AddCardToHand(card);
				sceneEventsWrapper.SceneCardEvents.RaiseCardAddedToHandEvent(card, false);

				card = cardFactory.CreateTransportActionCard();
				logicCardStateManager.AddCardToHand(card);
				sceneEventsWrapper.SceneCardEvents.RaiseCardAddedToHandEvent(card, false);
			}

			ProtoCardData retreiveCard = cardFactory.CreateRetreiveAndProduceCard();
			logicCardStateManager.AddCardToHand(retreiveCard);
			sceneEventsWrapper.SceneCardEvents.RaiseCardAddedToHandEvent(retreiveCard, false);
		}
	}
}
