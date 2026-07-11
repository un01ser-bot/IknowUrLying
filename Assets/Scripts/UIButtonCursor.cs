using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonCursor :
MonoBehaviour,
IPointerEnterHandler,
IPointerExitHandler,
IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorController.Instance.HoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorController.Instance.HoverExit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CursorController.Instance.Click();
    }
}