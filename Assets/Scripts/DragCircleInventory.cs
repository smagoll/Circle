using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCircleInventory : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        var menu = GameObject.FindGameObjectWithTag("menu");

        eventData.pointerDrag.transform.SetParent(menu.transform);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("cell_deck"))
        {
            eventData.pointerCurrentRaycast.gameObject.GetComponent<CellDeck>().fixedCircle = gameObject;
        }
        else
        {
            var myCircles = GameObject.FindGameObjectWithTag("my_circles");
            eventData.pointerDrag.transform.SetParent(myCircles.transform);
            //eventData.pointerDrag.transform.SetParent(gameObject.transform);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        
    }
}
