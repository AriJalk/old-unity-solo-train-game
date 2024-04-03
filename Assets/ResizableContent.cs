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
        //Resize(newObject);
    }

    public void Resize(RectTransform newObject, float modifier = 0)
    {
        float newX = newObject.position.x;
        if (newX < minX)
        {
            minX = newX;
        }
        else if (newX > maxX)
        {
            maxX = newX;
        }

        RectTransform.sizeDelta = new Vector2(maxX - modifier, RectTransform.sizeDelta.y);
    }
}
