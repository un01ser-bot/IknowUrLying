using UnityEngine;
using SojaExiles;

public class DoorPlayerAutoBinder : MonoBehaviour
{
    [Header("자동으로 찾을 플레이어 태그")]
    [SerializeField] private string playerTag = "Player";

    private void Awake()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag(playerTag);

        if (playerObject == null)
        {
            Debug.LogError(
                "DoorPlayerAutoBinder: Player 태그가 붙은 오브젝트를 찾지 못했습니다."
            );

            return;
        }

        Transform playerTransform = playerObject.transform;

        BindNormalDoors(playerTransform);
        BindDoor1s(playerTransform);
        BindStallDoors(playerTransform);
    }

    private void BindNormalDoors(Transform playerTransform)
    {
        opencloseDoor[] doors =
            FindObjectsByType<opencloseDoor>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (opencloseDoor door in doors)
        {
            door.Player = playerTransform;
        }

        Debug.Log($"일반 문 {doors.Length}개 플레이어 연결 완료");
    }

    private void BindDoor1s(Transform playerTransform)
    {
        opencloseDoor1[] doors =
            FindObjectsByType<opencloseDoor1>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (opencloseDoor1 door in doors)
        {
            door.Player = playerTransform;
        }

        Debug.Log($"Door1 문 {doors.Length}개 플레이어 연결 완료");
    }

    private void BindStallDoors(Transform playerTransform)
    {
        opencloseStallDoor[] doors =
            FindObjectsByType<opencloseStallDoor>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (opencloseStallDoor door in doors)
        {
            door.Player = playerTransform;
        }

        Debug.Log($"화장실 문 {doors.Length}개 플레이어 연결 완료");
    }
}