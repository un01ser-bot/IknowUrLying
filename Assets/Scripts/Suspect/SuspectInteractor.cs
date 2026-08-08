using UnityEngine;

public class SuspectInteractor : MonoBehaviour
{
    [Header("상호작용 설정")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [Header("심문 UI")]
    [SerializeField] private SuspectChatUI suspectChatUI;

    private SuspectNPC currentSuspect;

    private void Update()
    {
        FindSuspect();

        if (currentSuspect != null &&
            Input.GetKeyDown(interactionKey))
        {
            InteractWithSuspect();
        }
    }

    private void FindSuspect()
    {
        currentSuspect = null;

        if (playerCamera == null)
        {
            return;
        }

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance))
        {
            currentSuspect =
                hit.collider.GetComponentInParent<SuspectNPC>();
        }
    }

    private void InteractWithSuspect()
    {
        SuspectProfile profile =
            currentSuspect.Profile;

        if (profile == null)
        {
            Debug.LogWarning(
                "이 용의자에게 SuspectProfile이 없습니다."
            );
            return;
        }

        if (suspectChatUI == null)
        {
            Debug.LogWarning(
                "SuspectChatUI가 연결되지 않았습니다."
            );
            return;
        }

        suspectChatUI.OpenChat(profile);
    }

    private void OnDrawGizmosSelected()
    {
        if (playerCamera == null)
        {
            return;
        }

        Gizmos.DrawRay(
            playerCamera.transform.position,
            playerCamera.transform.forward *
            interactionDistance
        );
    }
}