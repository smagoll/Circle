using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIcon : MonoBehaviour
{
    public int id;
    public bool isHave = false;
    public bool isTake = false;

    void Start()
    {

    }

    private void Awake()
    {
        GetInfo();
        if (!isHave)// && !isTake
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            GetComponent<CanvasGroup>().alpha = 0.5f;
        }
    }

    private void GetInfo()//получение информации о circle
    {
        foreach (var circle in StartMenu.circles)
        {
            if (circle.Name == gameObject.name)
            {
                id = circle.Id;
                isHave = circle.IsHave;
                isTake = circle.IsTake;
            }
        }
    }
}
