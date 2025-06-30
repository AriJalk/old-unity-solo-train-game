using CardSystem;
using GameEngine.StateMachine;
using System;

namespace PrototypeGame.StateMachine.CommonStates
{
	internal class CardDragAndDropState : IStateMachine
	{
		private CardServices _cardServices;
		private ICardDropTarget _cardDropTarget;

		public Action<Guid> OnDropHandler { get; set; }

		public CardDragAndDropState(CardServices cardServices, ICardDropTarget cardDropTarget)
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
			if (_cardDropTarget != null)
			{
				_cardDropTarget.SetActive(false);
			}
		}

		private void OnDragStarted()
		{
			if (_cardDropTarget != null)
			{
				_cardDropTarget.SetActive(true);
			}

		}

		private void OnDragEnded()
		{
			if (_cardDropTarget != null)
			{
				_cardDropTarget.SetActive(false);
			}
		}

		private void OnCardDropped(Guid cardId)
		{
			OnDropHandler?.Invoke(cardId);
		}
	}
}
