using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class CapsulePlayerMove : MonoBehaviour
{
    [Header("이동")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float gravity = -20f;

    [Header("카메라")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 80f;

    private CharacterController characterController;

    private float verticalVelocity;
    private float cameraPitch;

    // true면 1인칭 조작 중
    private bool firstPersonControlEnabled = true;

    private void Awake()
    {
        characterController =
            GetComponent<CharacterController>();
    }

    private void Start()
    {
        EnterFirstPersonMode();
    }

    private void Update()
    {
        HandleCursorMode();

        if (!firstPersonControlEnabled)
        {
            return;
        }

        Move();
        Look();
    }

    private void Move()
    {
        float horizontal =
            Input.GetAxisRaw("Horizontal");

        float vertical =
            Input.GetAxisRaw("Vertical");

        Vector3 moveDirection =
            transform.right * horizontal +
            transform.forward * vertical;

        moveDirection =
            Vector3.ClampMagnitude(moveDirection, 1f);

        if (characterController.isGrounded &&
            verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMovement =
            moveDirection * moveSpeed;

        finalMovement.y = verticalVelocity;

        characterController.Move(
            finalMovement * Time.deltaTime
        );
    }

    private void Look()
    {
        float mouseX =
            Input.GetAxis("Mouse X") *
            mouseSensitivity;

        float mouseY =
            Input.GetAxis("Mouse Y") *
            mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;

        cameraPitch = Mathf.Clamp(
            cameraPitch,
            -maxLookAngle,
            maxLookAngle
        );

        if (cameraTransform != null)
        {
            cameraTransform.localRotation =
                Quaternion.Euler(
                    cameraPitch,
                    0f,
                    0f
                );
        }
    }

    private void HandleCursorMode()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitFirstPersonMode();
            return;
        }

        // 커서가 풀린 상태에서 왼쪽 클릭 시
        if (!firstPersonControlEnabled &&
            Input.GetMouseButtonDown(0))
        {
            bool pointerOverUI =
                EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject();

            // UI 위를 클릭한 경우에는 1인칭 모드로 돌아가지 않음
            if (pointerOverUI)
            {
                return;
            }

            EnterFirstPersonMode();
        }
    }

    private void EnterFirstPersonMode()
    {
        firstPersonControlEnabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (CursorController.Instance != null)
        {
            CursorController.Instance.HideCustomCursor();
        }
    }

    private void ExitFirstPersonMode()
    {
        firstPersonControlEnabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        if (CursorController.Instance != null)
        {
            CursorController.Instance.ShowCustomCursor();
        }
    }

    private void OnDisable()
    {
        // 플레이어 오브젝트가 꺼지거나
        // 다른 씬으로 이동할 때 커서를 다시 UI 모드로 복구
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        if (CursorController.Instance != null)
        {
            CursorController.Instance.ShowCustomCursor();
        }
    }
}