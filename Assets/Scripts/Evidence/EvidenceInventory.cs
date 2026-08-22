using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EvidenceInventory : MonoBehaviour
{
    public static EvidenceInventory Instance { get; private set; }

    [Header("증거 데이터베이스")]
    [SerializeField] private EvidenceDatabase evidenceDatabase;

    [Header("저장 파일 이름")]
    [SerializeField] private string saveFileName = "evidence_save.json";

    private readonly List<string> collectedEvidenceIds = new List<string>();

    public event Action<EvidenceData> OnEvidenceCollected;

    public EvidenceDatabase Database => evidenceDatabase;

    private string SavePath =>
        Path.Combine(Application.persistentDataPath, saveFileName);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadEvidence();
    }

    public bool CollectEvidence(EvidenceData evidence)
    {
        if (evidence == null)
        {
            Debug.LogWarning("수집하려는 EvidenceData가 없습니다.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(evidence.evidenceId))
        {
            Debug.LogError(
                $"증거 '{evidence.name}'에 Evidence ID가 없습니다."
            );

            return false;
        }

        if (HasEvidence(evidence.evidenceId))
        {
            Debug.Log($"이미 수집한 증거입니다: {evidence.evidenceName}");
            return false;
        }

        collectedEvidenceIds.Add(evidence.evidenceId);

        SaveEvidence();

        Debug.Log($"증거 수집: {evidence.evidenceName}");

        OnEvidenceCollected?.Invoke(evidence);

        return true;
    }

    public bool HasEvidence(string evidenceId)
    {
        return collectedEvidenceIds.Contains(evidenceId);
    }

    public List<EvidenceData> GetCollectedEvidence()
    {
        List<EvidenceData> result = new List<EvidenceData>();

        if (evidenceDatabase == null)
        {
            Debug.LogError("Evidence Database가 연결되지 않았습니다.");
            return result;
        }

        foreach (string evidenceId in collectedEvidenceIds)
        {
            EvidenceData evidence =
                evidenceDatabase.GetEvidence(evidenceId);

            if (evidence != null)
            {
                result.Add(evidence);
            }
            else
            {
                Debug.LogWarning(
                    $"데이터베이스에서 증거를 찾지 못했습니다: {evidenceId}"
                );
            }
        }

        return result;
    }

    public void SaveEvidence()
    {
        EvidenceSaveData saveData = new EvidenceSaveData
        {
            collectedEvidenceIds =
                new List<string>(collectedEvidenceIds)
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
    }

    public void LoadEvidence()
    {
        collectedEvidenceIds.Clear();

        if (!File.Exists(SavePath))
        {
            return;
        }

        try
        {
            string json = File.ReadAllText(SavePath);

            EvidenceSaveData saveData =
                JsonUtility.FromJson<EvidenceSaveData>(json);

            if (saveData?.collectedEvidenceIds != null)
            {
                collectedEvidenceIds.AddRange(
                    saveData.collectedEvidenceIds
                );
            }
        }
        catch (Exception exception)
        {
            Debug.LogError(
                $"증거 저장 파일을 불러오지 못했습니다.\n{exception}"
            );
        }
    }

    [ContextMenu("수집한 증거 초기화")]
    public void ClearCollectedEvidence()
    {
        Debug.Log("증거 초기화 버튼 실행됨");

        collectedEvidenceIds.Clear();

        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log($"증거 저장 파일 삭제 완료: {SavePath}");
        }
        else
        {
            Debug.Log("삭제할 증거 저장 파일이 없습니다.");
        }

        Debug.Log("수집한 증거를 모두 초기화했습니다.");
    }
}

[Serializable]
public class EvidenceSaveData
{
    public List<string> collectedEvidenceIds = new List<string>();
}