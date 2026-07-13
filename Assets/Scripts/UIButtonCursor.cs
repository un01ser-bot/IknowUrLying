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
        if (CursorController.Instance != null)
            CursorController.Instance.HoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CursorController.Instance != null)
            CursorController.Instance.HoverExit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CursorController.Instance != null)
            CursorController.Instance.Click();
    }
}
