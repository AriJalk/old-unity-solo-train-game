using Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRaycastTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GraphicUserInterface.IsMouseOver = true;
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GraphicUserInterface.IsMouseOver = false;
        Debug.Log("Exit");
    }
}
