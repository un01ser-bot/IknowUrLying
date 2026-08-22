using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance;

    private RectTransform rect;
    private bool isHovering;

    // true면 일반 UI용 돋보기 커서 모드
    // false면 1인칭 플레이 모드
    private bool customCursorEnabled = true;

    [Header("Settings")]
    public float normalScale = 1f;
    public float hoverScale = 1.08f;
    public float smoothSpeed = 15f;

    private Vector3 targetScale;
    private float targetRotation;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        rect = GetComponent<RectTransform>();

        MoveToPersistentCursorCanvas();

        targetScale = Vector3.one * normalScale;

        EnableCustomCursor();
    }

    private void OnEnable()
    {
        if (customCursorEnabled)
        {
            HideSystemCursorForUI();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            return;
        }

        if (customCursorEnabled)
        {
            HideSystemCursorForUI();
        }
        else
        {
            LockCursorForFirstPerson();
        }
    }

    private void Update()
    {
        if (!customCursorEnabled)
        {
            return;
        }

        HideSystemCursorForUI();

        if (rect != null)
        {
            rect.position = Input.mousePosition;

            rect.localScale = Vector3.Lerp(
                rect.localScale,
                targetScale,
                Time.deltaTime * smoothSpeed
            );

            Quaternion targetRot = Quaternion.Euler(
                0f,
                0f,
                targetRotation
            );

            rect.rotation = Quaternion.Lerp(
                rect.rotation,
                targetRot,
                Time.deltaTime * smoothSpeed
            );
        }
    }

    public void EnableCustomCursor()
    {
        customCursorEnabled = true;

        gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        isHovering = false;
        targetScale = Vector3.one * normalScale;
        targetRotation = 0f;

        if (rect != null)
        {
            rect.position = Input.mousePosition;
        }
    }

    public void DisableCustomCursor()
    {
        customCursorEnabled = false;

        isHovering = false;
        targetScale = Vector3.one * normalScale;
        targetRotation = 0f;

        // 커서 이미지 자체만 숨김
        // 게임 오브젝트 전체를 끄면 싱글톤 호출이 불편할 수 있어서
        // CanvasRenderer를 이용해 투명하게 처리
        SetCursorImageVisible(false);

        LockCursorForFirstPerson();
    }

    public void ShowCustomCursor()
    {
        customCursorEnabled = true;

        SetCursorImageVisible(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        isHovering = false;
        targetScale = Vector3.one * normalScale;
        targetRotation = 0f;

        if (rect != null)
        {
            rect.position = Input.mousePosition;
        }
    }

    public void HideCustomCursor()
    {
        customCursorEnabled = false;

        SetCursorImageVisible(false);

        LockCursorForFirstPerson();
    }

    public bool IsCustomCursorEnabled()
    {
        return customCursorEnabled;
    }

    public void HoverEnter()
    {
        if (!customCursorEnabled)
        {
            return;
        }

        isHovering = true;
        targetScale = Vector3.one * hoverScale;
    }

    public void HoverExit()
    {
        if (!customCursorEnabled)
        {
            return;
        }

        isHovering = false;
        targetScale = Vector3.one * normalScale;
    }

    public void Click()
    {
        if (!customCursorEnabled)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(ClickAnim());
    }

    private IEnumerator ClickAnim()
    {
        targetScale = Vector3.one * (hoverScale * 0.93f);
        targetRotation = -4f;

        yield return new WaitForSecondsRealtime(0.06f);

        targetRotation = 0f;

        targetScale = Vector3.one *
            (isHovering ? hoverScale : normalScale);
    }

    private void HideSystemCursorForUI()
    {
        Cursor.visible = false;

        // UI 모드일 때만 마우스 잠금 해제
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        SetCursorImageVisible(true);
    }

    private void LockCursorForFirstPerson()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetCursorImageVisible(bool visible)
    {
        Image image = GetComponent<Image>();

        if (image != null)
        {
            image.enabled = visible;
        }
    }

    private void MoveToPersistentCursorCanvas()
    {
        Canvas currentCanvas = GetComponentInParent<Canvas>();

        if (currentCanvas != null &&
            currentCanvas.gameObject.name == "PersistentCursorCanvas")
        {
            DontDestroyOnLoad(currentCanvas.gameObject);
            return;
        }

        GameObject canvasObject =
            new GameObject("PersistentCursorCanvas");

        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        CanvasScaler scaler =
            canvasObject.AddComponent<CanvasScaler>();

        scaler.uiScaleMode =
            CanvasScaler.ScaleMode.ConstantPixelSize;

        transform.SetParent(canvasObject.transform, false);
        transform.SetAsLastSibling();

        DontDestroyOnLoad(canvasObject);
    }
}