using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceArchiveUI : MonoBehaviour
{
    [Header("증거 목록")]
    [SerializeField] private Transform contentParent;
    [SerializeField] private EvidenceListItemUI evidenceItemPrefab;

    [Header("증거 상세 정보")]
    [SerializeField] private EvidenceDetailUI evidenceDetailUI;

    [Header("목록 설정")]
    [SerializeField] private bool selectFirstEvidenceAutomatically = true;

    private readonly List<GameObject> spawnedItems =
        new List<GameObject>();

    private Coroutine refreshCoroutine;

    private void OnEnable()
    {
        refreshCoroutine = StartCoroutine(RefreshNextFrame());
    }

    private void OnDisable()
    {
        if (refreshCoroutine != null)
        {
            StopCoroutine(refreshCoroutine);
            refreshCoroutine = null;
        }
    }

    private IEnumerator RefreshNextFrame()
    {
        // EvidenceInventory의 Awake가 먼저 실행될 시간을 줌
        yield return null;

        Refresh();
        refreshCoroutine = null;
    }

    public void Refresh()
    {
        ClearList();

        if (EvidenceInventory.Instance == null)
        {
            Debug.LogWarning(
                "EvidenceInventory가 없습니다. " +
                "조수방 씬에 EvidenceSystem 오브젝트와 " +
                "EvidenceInventory 컴포넌트가 있는지 확인하세요."
            );

            return;
        }

        if (contentParent == null)
        {
            Debug.LogError(
                "EvidenceArchiveUI의 Content Parent가 비어 있습니다."
            );

            return;
        }

        if (evidenceItemPrefab == null)
        {
            Debug.LogError(
                "EvidenceArchiveUI의 Evidence Item Prefab이 비어 있습니다."
            );

            return;
        }

        List<EvidenceData> collectedEvidence =
            EvidenceInventory.Instance.GetCollectedEvidence();

        foreach (EvidenceData evidence in collectedEvidence)
        {
            EvidenceListItemUI item = Instantiate(
                evidenceItemPrefab,
                contentParent
            );

            item.Setup(evidence, SelectEvidence);
            spawnedItems.Add(item.gameObject);
        }

        if (selectFirstEvidenceAutomatically &&
            collectedEvidence.Count > 0)
        {
            SelectEvidence(collectedEvidence[0]);
        }
        else if (evidenceDetailUI != null)
        {
            evidenceDetailUI.Clear();
        }

        Debug.Log(
            $"조수방 증거 목록 생성 완료: {collectedEvidence.Count}개"
        );
    }

    private void SelectEvidence(EvidenceData evidence)
    {
        if (evidenceDetailUI == null)
        {
            Debug.LogWarning(
                "Evidence Detail UI가 연결되지 않았습니다."
            );

            return;
        }

        evidenceDetailUI.ShowEvidence(evidence);
    }

    private void ClearList()
    {
        foreach (GameObject item in spawnedItems)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }

        spawnedItems.Clear();
    }
}