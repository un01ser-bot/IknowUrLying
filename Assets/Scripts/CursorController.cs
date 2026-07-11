using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance;

    RectTransform rect;

    [Header("Settings")]
    public float normalScale = 1f;
    public float hoverScale = 1.08f;
    public float smoothSpeed = 15f;

    Vector3 targetScale;
    float targetRotation;

    void Awake()
    {
        Instance = this;

        rect = GetComponent<RectTransform>();

        Cursor.visible = false;

        targetScale = Vector3.one * normalScale;
    }

    private void Start()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
    }

    void Update()
    {
        rect.position = Input.mousePosition;

        rect.localScale = Vector3.Lerp(
            rect.localScale,
            targetScale,
            Time.deltaTime * smoothSpeed);

        Quaternion targetRot =
            Quaternion.Euler(0, 0, targetRotation);

        rect.rotation = Quaternion.Lerp(
            rect.rotation,
            targetRot,
            Time.deltaTime * smoothSpeed);
    }

    public void HoverEnter()
    {
        targetScale = Vector3.one * hoverScale;
    }

    public void HoverExit()
    {
        targetScale = Vector3.one * normalScale;
    }

    public void Click()
    {
        StopAllCoroutines();
        StartCoroutine(ClickAnim());
    }

    System.Collections.IEnumerator ClickAnim()
    {
        targetScale = Vector3.one * (hoverScale * 0.93f);
        targetRotation = -4f;

        yield return new WaitForSeconds(0.06f);

        targetRotation = 0f;

        targetScale = Vector3.one *
            (hoverScale == targetScale.x ? hoverScale : normalScale);
    }
}