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
		private CardObjectServices _cardServices;
		private SceneCardEvents _sceneCardEvents;

		private SceneCardState _sceneCardState;

		private SceneCardStateManipulator _sceneCardStateManipulator;

		public SceneCardStateManager(CommonServices commonServices, CardObjectServices cardServices, SceneEventsWrapper sceneEventsWrapper)
		{
			_commonServices = commonServices;
			_cardServices = cardServices;
			_sceneCardEvents = sceneEventsWrapper.SceneCardEvents;

			_sceneCardState = new SceneCardState();

			_sceneCardStateManipulator = new SceneCardStateManipulator(_commonServices, _cardServices);

			_sceneCardEvents.CardAddedToHandEvent += OnCardAddedToHand;
			_sceneCardEvents.CardRemovedFromHandEvent += OnCardRemovedFromHand;
		}

		public void Dispose()
		{
			_sceneCardEvents.CardAddedToHandEvent -= OnCardAddedToHand;
			_sceneCardEvents.CardRemovedFromHandEvent -= OnCardRemovedFromHand;
		}

		private void OnCardAddedToHand(ProtoCardData cardData)
		{
			ProtoCardObject cardObject = _sceneCardStateManipulator.BuildCard(cardData);

			_sceneCardState.Cards.Add(cardObject.guid, cardObject);

			_sceneCardStateManipulator.AddCardToHand(cardObject);
		}

		private void OnCardRemovedFromHand(Guid cardId)
		{
			ProtoCardObject cardObject = _sceneCardState.Cards[cardId];

			_sceneCardState.Cards.Remove(cardId);

			_sceneCardStateManipulator.RemoveCardFromHand(cardObject);
		}
	}
}
