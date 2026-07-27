using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EvidenceDatabase",
    menuName = "Detective Game/Evidence Database"
)]
public class EvidenceDatabase : ScriptableObject
{
    [Header("게임에 존재하는 모든 증거")]
    public List<EvidenceData> allEvidence = new List<EvidenceData>();

    private Dictionary<string, EvidenceData> evidenceDictionary;

    private void OnEnable()
    {
        BuildDictionary();
    }

    public void BuildDictionary()
    {
        evidenceDictionary = new Dictionary<string, EvidenceData>();

        foreach (EvidenceData evidence in allEvidence)
        {
            if (evidence == null)
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(evidence.evidenceId))
            {
                Debug.LogWarning(
                    $"증거 '{evidence.name}'에 Evidence ID가 없습니다."
                );

                continue;
            }

            if (evidenceDictionary.ContainsKey(evidence.evidenceId))
            {
                Debug.LogError(
                    $"중복된 Evidence ID가 있습니다: {evidence.evidenceId}"
                );

                continue;
            }

            evidenceDictionary.Add(evidence.evidenceId, evidence);
        }
    }

    public EvidenceData GetEvidence(string evidenceId)
    {
        if (evidenceDictionary == null)
        {
            BuildDictionary();
        }

        evidenceDictionary.TryGetValue(evidenceId, out EvidenceData evidence);
        return evidence;
    }
}