using UnityEngine;

namespace CommonEngine.SceneServices
{
	public static class SceneHelpers
	{
		public static void SetParentAndResetPosition(Transform child, Transform parent)
		{
			child.SetParent(parent);
			child.localPosition = Vector3.zero;
		}
	}
}
