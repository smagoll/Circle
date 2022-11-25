using UnityEngine;
using UnityEngine.EventSystems;

public class CirclesMenu : ButtonNavigation
{
    [SerializeField] private GameObject circlesMenu;

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (IsDown)
        {
            StartMenu.SetMenu(circlesMenu);
            GlobalEventManager.StartPressCircles();
        }
    }
}
