using CardSystem;
using System;
using TurnBasedHexEngine.StateMachine;

namespace PrototypeGame.StateMachine.CommonStates
{
	internal class CardDragAndDropState : IStateMachine
	{
		public event Action<Guid> OnCardDroppedEvent;

		private CardObjectServices _cardServices;
		private ICardDropTarget _cardDropTarget;


		public CardDragAndDropState(CardObjectServices cardServices, ICardDropTarget cardDropTarget)
		{
			_cardServices = cardServices;
			_cardDropTarget = cardDropTarget;
		}

		public void EnterState()
		{
			_cardServices.DragStartedEvent += OnDragStarted;
			_cardServices.DragEndedEvent += OnDragEnded;
			_cardDropTarget.OnCardDropEvent += OnCardDropped;
		}

		public void ExitState()
		{
			_cardServices.DragStartedEvent -= OnDragStarted;
			_cardServices.DragEndedEvent -= OnDragEnded;
			_cardDropTarget.OnCardDropEvent -= OnCardDropped;

			_cardDropTarget?.SetActive(false);
		}

		private void OnDragStarted()
		{
			_cardDropTarget?.SetActive(true);
		}

		private void OnDragEnded()
		{
			_cardDropTarget?.SetActive(false);
		}

		private void OnCardDropped(Guid cardId)
		{
			OnCardDroppedEvent?.Invoke(cardId);
		}
	}
}
