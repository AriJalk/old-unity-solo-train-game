using CardSystem;
using TurnBasedHexEngine.StateMachine;
using System;

namespace PrototypeGame.StateMachine.CommonStates
{
	internal class CardDragAndDropState : IStateMachine
	{
		private CardObjectServices _cardServices;
		private ICardDropTarget _cardDropTarget;

		public Action<Guid> OnDropHandler { get; set; }

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
			OnDropHandler?.Invoke(cardId);
		}
	}
}
