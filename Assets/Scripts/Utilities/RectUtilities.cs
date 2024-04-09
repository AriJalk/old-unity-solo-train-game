using UnityEngine;

public static class RectUtilities
{
    public static void ResetAnchorsAndSize(RectTransform rectTransform)
    {
        if (rectTransform != null)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;

        }
    }

    public static void SetAnchorsAndResetSize(RectTransform rectTransform, Vector2 min, Vector2 max)
    {
        if (rectTransform != null)
        {
            rectTransform.anchorMin = min;
            rectTransform.anchorMax = max;
            ResetSize(rectTransform);
        }
    }

    public static void ResetSize(RectTransform rectTransform)
    {
        if (rectTransform != null )
        {
            rectTransform.sizeDelta = Vector2.zero;
        }
    }

    public static void SetParentAndResetPosition(Transform child, Transform parent)
    {
        if (child != null && parent != null)
        {
            child.SetParent(parent, false);
            child.localPosition = Vector3.zero;
            child.localScale = Vector3.one;
            ResetSize(child.GetComponent<RectTransform>());
        }
    }
}