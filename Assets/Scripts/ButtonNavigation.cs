using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class ButtonNavigation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
    public bool IsDown;
    public GameObject pressButton;
    private GameObject[] buttonsNavigationExpectCurrent;// ������, ����� �������

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsDown)
        {
            return;
        }
        pressButton = eventData.pointerCurrentRaycast.gameObject;
        buttonsNavigationExpectCurrent = StartMenu.buttonsNavigation.Where(x => x.name != gameObject.name)
                                           .ToArray();
        foreach (var btn in buttonsNavigationExpectCurrent)//��������� ����� ���������� ������ 
        {
            btn.GetComponent<Image>().color = new Color32(180, 180, 180, 255);
        }
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);//��������� ����� �������� ������
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (IsDown)//�������� ������ �� ������ 
        {
            return;
        }
        if (pressButton == eventData.pointerCurrentRaycast.gameObject)// ���� ������� ����������� �� ������
        {
            IsDown = true;
            foreach (var btn in buttonsNavigationExpectCurrent)
            {
                btn.GetComponent<ButtonNavigation>().IsDown = false;
            }
        }
        else// ���� ������� ����������� �� �� ������
        {
            IsDown = false;
            foreach (var btn in buttonsNavigationExpectCurrent)
            {
                btn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            GetComponent<Image>().color = new Color32(180, 180, 180, 255);
        }
    }
}
