using UnityEngine;
using UnityEngine.EventSystems;

public class PlayMenu : ButtonNavigation
{
    [SerializeField] private GameObject playMenu;
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (IsDown)
        {
            StartMenu.SetMenu(playMenu);
            GlobalEventManager.StartPressPlay();
        }
    }
}
