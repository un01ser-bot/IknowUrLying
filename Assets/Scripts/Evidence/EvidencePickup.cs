using UnityEngine;

public class EvidencePickup : MonoBehaviour
{
    [Header("이 오브젝트의 증거 정보")]
    [SerializeField] private EvidenceData evidenceData;

    [Header("플레이어 거리 제한")]
    [SerializeField] private float interactionDistance = 5f;

    [Header("수집 후 처리")]
    [SerializeField] private bool hideAfterCollect = true;

    private Transform player;
    private bool isCollected;

    private void Start()
    {
        FindPlayer();

        if (EvidenceInventory.Instance != null &&
            evidenceData != null &&
            EvidenceInventory.Instance.HasEvidence(
                evidenceData.evidenceId
            ))
        {
            ApplyCollectedState();
        }
    }

    private void FindPlayer()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning(
                $"Player 태그 오브젝트를 찾지 못했습니다: {name}"
            );
        }
    }

    private void OnMouseOver()
    {
        if (isCollected)
        {
            return;
        }

        if (player == null)
        {
            FindPlayer();

            if (player == null)
            {
                return;
            }
        }

        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance > interactionDistance)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            TryCollect();
        }
    }

    public virtual void TryCollect()
    {
        if (EvidenceInventory.Instance == null)
        {
            Debug.LogError(
                "씬에 EvidenceInventory가 없습니다."
            );

            return;
        }

        if (evidenceData == null)
        {
            Debug.LogError(
                $"EvidenceData가 연결되지 않았습니다: {name}"
            );

            return;
        }

        bool collected =
            EvidenceInventory.Instance.CollectEvidence(evidenceData);

        if (collected)
        {
            ApplyCollectedState();
        }
    }

    protected virtual void ApplyCollectedState()
    {
        isCollected = true;

        if (hideAfterCollect)
        {
            gameObject.SetActive(false);
        }
    }
}