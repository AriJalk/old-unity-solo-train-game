using UnityEngine;
using UnityEngine.Events;

namespace CommonEngine.IO
{
	public class InputEvents
	{
		#region PhysicalInput
		public readonly UnityEvent<int, Vector2> MouseButtonClickedDownEvent = new UnityEvent<int, Vector2>();
		public readonly UnityEvent<int, Vector2> MouseButtonClickedUpEvent = new UnityEvent<int, Vector2>();
		public readonly UnityEvent<Vector2> AxisMovedEvent = new UnityEvent<Vector2>();
		public readonly UnityEvent<float> MouseScrolledEvent = new UnityEvent<float>();
		#endregion

		#region UIEvents
		public readonly UnityEvent<Vector2> WorldDraggedEvent = new UnityEvent<Vector2>();

		#endregion

	}
}