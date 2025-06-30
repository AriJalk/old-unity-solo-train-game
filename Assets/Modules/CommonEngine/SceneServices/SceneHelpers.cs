using UnityEngine;

namespace CommonEngine.SceneServices
{
	/// <summary>
	/// General tools to help reduce repeated code regarding scene manipulation
	/// </summary>
	public static class SceneHelpers
	{
		public static void SetParentAndResetPosition(Transform child, Transform parent)
		{
			child.SetParent(parent, true);
			child.localPosition = Vector3.zero;
		}

		public static void InitializeRectObject(RectTransform child, RectTransform parent)
		{
			child.SetParent(parent, false);

			child.anchorMin = Vector2.zero;
			child.anchorMax = Vector2.one;

			child.offsetMin = Vector2.zero;
			child.offsetMax = Vector2.zero;

			child.localPosition = Vector3.zero;
		}
	}
}
