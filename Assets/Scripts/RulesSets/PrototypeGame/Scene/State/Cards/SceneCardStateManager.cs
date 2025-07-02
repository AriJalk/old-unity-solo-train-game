using CardSystem;
using CommonEngine.Core;
using PrototypeGame.Events;
using PrototypeGame.Logic.Components.Cards;
using PrototypeGame.Scene.Components.Cards;
using System;

namespace PrototypeGame.Scene.State.Cards
{
	internal class SceneCardStateManager : IDisposable
	{
		private CommonServices _commonServices;
		private CardObjectServices _cardObjectServices;
		private SceneCardEvents _sceneCardEvents;

		private SceneCardState _sceneCardState;

		private SceneCardStateManipulator _sceneCardStateManipulator;

		public SceneCardStateManager(CommonServices commonServices, CardObjectServices cardServices, SceneEventsWrapper sceneEventsWrapper)
		{
			_commonServices = commonServices;
			_cardObjectServices = cardServices;
			_sceneCardEvents = sceneEventsWrapper.SceneCardEvents;

			_sceneCardState = new SceneCardState();

			_sceneCardStateManipulator = new SceneCardStateManipulator(_commonServices, _cardObjectServices);

			_sceneCardEvents.CardAddedToHandEvent += OnCardAddedToHand;
			_sceneCardEvents.CardRemovedFromHandEvent += OnCardRemovedFromHand;

			_sceneCardEvents.CardsInHandReorganizedEvent += OnCardsInHandReorganized;

		}


		public void Dispose()
		{
			_sceneCardEvents.CardAddedToHandEvent -= OnCardAddedToHand;
			_sceneCardEvents.CardRemovedFromHandEvent -= OnCardRemovedFromHand;
			_sceneCardEvents.CardsInHandReorganizedEvent -= OnCardsInHandReorganized;
		}

		private void OnCardAddedToHand(ProtoCardData cardData, bool fromUndo)
		{
			ProtoCardObject cardObject = _sceneCardStateManipulator.BuildCard(cardData);
			_sceneCardState.Cards.Add(cardObject.guid, cardObject);
			_sceneCardStateManipulator.AddCardToHand(cardObject, fromUndo);
		}

		private void OnCardRemovedFromHand(Guid cardId, bool fromUndo)
		{
			ProtoCardObject cardObject = _sceneCardState.Cards[cardId];
			_sceneCardState.Cards.Remove(cardId);
			_sceneCardStateManipulator.RemoveCardFromHand(cardObject, fromUndo);
		}


		private void OnCardsInHandReorganized()
		{
			_sceneCardStateManipulator.ReorganizeCardsInHand();
		}
	}
}
