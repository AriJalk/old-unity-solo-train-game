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
		private CardServices _cardServices;
		private SceneCardEvents _sceneCardEvents;

		private SceneCardState _sceneCardState;

		private SceneCardStateManipulator _sceneCardStateManipulator;

		public SceneCardStateManager(CommonServices commonServices, CardServices cardServices, GameStateEvents gameStateEvents)
		{
			_commonServices = commonServices;
			_cardServices = cardServices;
			_sceneCardEvents = gameStateEvents.SceneCardEvents;

			_sceneCardState = new SceneCardState();

			_sceneCardStateManipulator = new SceneCardStateManipulator(_commonServices, _cardServices);

			_sceneCardEvents.CardCreatedAndAddedToHandEvent += OnCardCreatedAndAddedToHand;
		}

		public void Dispose()
		{
			_sceneCardEvents.CardCreatedAndAddedToHandEvent -= OnCardCreatedAndAddedToHand;
		}

		private void OnCardCreatedAndAddedToHand(ProtoCardData cardData)
		{
			ProtoCardObject cardObject = _sceneCardStateManipulator.BuildCard(cardData);

			_sceneCardState.Cards.Add(cardObject.guid, cardObject);

			_sceneCardStateManipulator.AddCardToHand(cardObject);
		}
	}
}
