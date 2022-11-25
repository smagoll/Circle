using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellDeck : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform my_circles;
    public GameObject fixedCircle;

    public void OnDrop(PointerEventData eventData)
    {
        if (fixedCircle != null)
        {
            DataBase.ExecuteQueryWithAnswer($"UPDATE circles SET is_take = false WHERE id_circle = {fixedCircle.GetComponent<CircleIcon>().id}");
            fixedCircle.transform.SetParent(my_circles);
            fixedCircle.GetComponent<CanvasGroup>().blocksRaycasts = true;
            //GlobalEventManager.StartPressCircles();
        }
        eventData.pointerDrag.transform.position = gameObject.transform.position;
        eventData.pointerDrag.transform.SetParent(gameObject.transform);
        SendDatabaseCircleCelldeck(eventData);
    }

    private void SendDatabaseCircleCelldeck(PointerEventData eventData)//отправка колоды в базу данных
    {
        var idTakeCircle = eventData.pointerDrag.GetComponent<CircleIcon>().id;
        var name_celldeck = gameObject.name;
        DataBase.ExecuteQueryWithAnswer($"UPDATE main_deck SET circle = {idTakeCircle} WHERE name_cell = '{name_celldeck}'");
        DataBase.ExecuteQueryWithAnswer($"UPDATE circles SET is_take = true WHERE id_circle = {idTakeCircle}");
    }
}
