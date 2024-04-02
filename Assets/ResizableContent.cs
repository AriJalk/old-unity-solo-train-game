using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableContent : MonoBehaviour
{
    public RectTransform RectTransform;
    private float minX = 0;
    private float maxX = 0;

    public void AddToTransform(Transform newObject)
    {
        newObject.SetParent(RectTransform);
        Resize(newObject);
    }

    public void Resize(Transform newObject)
    {
        if (newObject.position.x < minX)
        {
            minX = newObject.position.x;
        }
        else if (newObject.position.x > maxX)
        {
            maxX = newObject.position.x;
        }

        RectTransform.sizeDelta = new Vector2(maxX, RectTransform.sizeDelta.y);
    }
}
