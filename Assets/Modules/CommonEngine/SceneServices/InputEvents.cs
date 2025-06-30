using System;
using UnityEngine;

namespace CommonEngine.Events
{
	/// <summary>
	/// The exposed API for the core input system
	/// </summary>
	public class InputEvents
	{
		#region PhysicalInput

		public event Action<int, Vector2> MouseButtonClickedDownEvent;
		public event Action<int, Vector2> MouseButtonClickedUpEvent;
		public event Action<float> MouseScrolledEvent;
		public event Action<Vector2> AxisMovedEvent;

		public void RaiseMouseButtonClickedDown(int button,  Vector2 pos)
		{
			MouseButtonClickedDownEvent?.Invoke(button, pos);
		}
		public void RaiseMouseButtonClickedUp(int button, Vector2 pos)
		{
			MouseButtonClickedUpEvent?.Invoke(button, pos);
		}
		public void RaiseMouseScrolledEvent(float scroll)
		{
			MouseScrolledEvent?.Invoke(scroll);
		}
		public void RaiseAxisMovedEvent(Vector2 movement)
		{
			AxisMovedEvent?.Invoke(movement);
		}

		#endregion

		#region UIEvents
		public event Action<Vector2> WorldDraggedEvent;

		public void RaiseWorldDraggedEvent(Vector2 dragVector)
		{
			WorldDraggedEvent?.Invoke(dragVector);
		}
		#endregion

	}
}