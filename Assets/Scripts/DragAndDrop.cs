using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject gameObjectSpawnDrop;
    private GameObject createdGameObject;

    public void FollowMouse()// ���������e �� ������
    {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GlobalEventManager.StartHideCircles(eventData.pointerDrag, true);// ������� ������������ ��� ���������� ��������
        createdGameObject = Instantiate(gameObject);// �������� ����������� ����� �������
        foreach (Transform child in createdGameObject.transform) Destroy(child.gameObject);// �������� �������� � �������

        createdGameObject.GetComponent<Circle>().parentCircle = eventData.pointerDrag;// ��������� ��������� � ����������� �����
        createdGameObject.GetComponent<BoxCollider2D>().enabled = false;//���������� boxCollider ������������
        createdGameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;//���������� blockRaycasts ������������
        createdGameObject.GetComponent<Circle>().IsShoot = false;// ���������� �������� ������������
        GetComponent<CanvasGroup>().blocksRaycasts = false;//���������� blockRaycasts ��������
        GetComponent<BoxCollider2D>().enabled = false;//���������� boxCollider ��������
        GetComponent<CanvasGroup>().alpha = 0.5f;
        //GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);// ��������� ������������
        eventData.pointerDrag = createdGameObject; // ����� ������� ��� ��������������
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
        
        if (dragCircleScript.level == standingCircleScript.level && dragCircleScript.id == standingCircleScript.id)//���� ��� ������
        {
            var startCircle = dragCircle.GetComponent<Circle>().parentCircle;//circle, ������� �������� ���������
            startCircle.GetComponent<Circle>().level++;//��������� ������
            Instantiate(startCircle, standingCircle.transform.position, Quaternion.identity);//�������� ������ circle
            Destroy(standingCircle);//�������� circle � ������� ���������
            Destroy(startCircle);//������� circle, ������� �������� ���������
        }
        else
            Debug.Log("error");
    }

}
