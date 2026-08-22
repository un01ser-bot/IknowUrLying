using UnityEngine;

public class ClickToiletLid : MonoBehaviour
{
    [Header("열리는 각도")]
    [SerializeField] private float openAngle = 90f;

    [Header("회전 속도")]
    [SerializeField] private float rotateSpeed = 5f;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private bool isOpen;

    private void Start()
    {
        closedRotation = transform.localRotation;

        openRotation = closedRotation * Quaternion.Euler(0f, 0f, openAngle);
    }

    private void Update()
    {
        Quaternion targetRotation = isOpen
            ? openRotation
            : closedRotation;

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
        {
            transform.localRotation = targetRotation;
        }
    }

    private void OnMouseDown()
    {
        isOpen = !isOpen;
    }
}