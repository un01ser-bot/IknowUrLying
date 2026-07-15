using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance;

    RectTransform rect;
    bool isHovering;

    [Header("Settings")]
    public float normalScale = 1f;
    public float hoverScale = 1.08f;
    public float smoothSpeed = 15f;

    Vector3 targetScale;
    float targetRotation;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        rect = GetComponent<RectTransform>();
        MoveToPersistentCursorCanvas();

        HideSystemCursor();

        targetScale = Vector3.one * normalScale;
    }

    void OnEnable()
    {
        HideSystemCursor();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
            HideSystemCursor();
    }

    void Update()
    {
        HideSystemCursor();

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
        isHovering = true;
        targetScale = Vector3.one * hoverScale;
    }

    public void HoverExit()
    {
        isHovering = false;
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

        targetScale = Vector3.one * (isHovering ? hoverScale : normalScale);
    }

    void HideSystemCursor()
    {
        if (Cursor.visible)
            Cursor.visible = false;

        if (Cursor.lockState != CursorLockMode.None)
            Cursor.lockState = CursorLockMode.None;
    }

    void MoveToPersistentCursorCanvas()
    {
        Canvas currentCanvas = GetComponentInParent<Canvas>();

        if (currentCanvas != null && currentCanvas.gameObject.name == "PersistentCursorCanvas")
        {
            DontDestroyOnLoad(currentCanvas.gameObject);
            return;
        }

        GameObject canvasObject = new GameObject("PersistentCursorCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

        transform.SetParent(canvasObject.transform, false);
        transform.SetAsLastSibling();

        DontDestroyOnLoad(canvasObject);
    }
}
