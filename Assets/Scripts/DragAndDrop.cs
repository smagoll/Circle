using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject gameObjectSpawnDrop;
    private GameObject createdGameObject;

    public void FollowMouse()// следованиe за мышкой
    {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GlobalEventManager.StartHideCircles(eventData.pointerDrag, true);// скрытие недопустимых для соеденения объектов
        createdGameObject = Instantiate(gameObject);// создание виртуальной копии объекта
        foreach (Transform child in createdGameObject.transform) Destroy(child.gameObject);// удаление снарядов с объекта

        createdGameObject.GetComponent<Circle>().parentCircle = eventData.pointerDrag;// установка оригинала в виртуальной копии
        createdGameObject.GetComponent<BoxCollider2D>().enabled = false;//выключение boxCollider виртуального
        createdGameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;//выключение blockRaycasts виртуального
        createdGameObject.GetComponent<Circle>().IsShoot = false;// выключение стрельбы виртуального
        GetComponent<CanvasGroup>().blocksRaycasts = false;//выключение blockRaycasts родителя
        GetComponent<BoxCollider2D>().enabled = false;//выключение boxCollider родителя
        GetComponent<CanvasGroup>().alpha = 0.5f;
        //GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);// установка прозрачности
        eventData.pointerDrag = createdGameObject; // смена объекта для перетаскивания
    }

    public void OnDrag(PointerEventData eventData)
    {
        FollowMouse();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(vector2, Vector2.zero);
        GlobalEventManager.StartHideCircles(eventData.pointerDrag, false);

        var startCircle = eventData.pointerDrag.GetComponent<Circle>().parentCircle;
        startCircle.GetComponent<CanvasGroup>().blocksRaycasts = true;
        startCircle.GetComponent<CanvasGroup>().alpha = 1;
        //startCircle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        startCircle.GetComponent<BoxCollider2D>().enabled = true;

        if (rayHit.collider != null)
        {
            Debug.Log(rayHit.collider.gameObject.name);
            CreatedNewCircle(eventData.pointerDrag, rayHit.collider.gameObject);
            //Destroy(startCircle);
        }
        Destroy(eventData.pointerDrag);
    }

    void CreatedNewCircle(GameObject dragCircle, GameObject standingCircle)
    {
        if (dragCircle.name != standingCircle.name)
        {
            return;
        }

        GlobalEventManager.StartHideCircles(dragCircle, false);
        var dragCircleScript = dragCircle.GetComponent<Circle>();
        var standingCircleScript = standingCircle.GetComponent<Circle>();
        
        if (dragCircleScript.level == standingCircleScript.level && dragCircleScript.id == standingCircleScript.id)//если они похожи
        {
            var startCircle = dragCircle.GetComponent<Circle>().parentCircle;//circle, который пытаемся соединить
            startCircle.GetComponent<Circle>().level++;//повышение уровня
            Instantiate(startCircle, standingCircle.transform.position, Quaternion.identity);//создание нового circle
            Destroy(standingCircle);//удаление circle с которым соединяем
            Destroy(startCircle);//удаляем circle, который пытаемся соединить
        }
        else
            Debug.Log("error");
    }

}
