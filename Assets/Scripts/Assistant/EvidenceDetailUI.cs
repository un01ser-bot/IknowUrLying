using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceDetailUI : MonoBehaviour
{
    [Header("상세 정보 UI")]
    [SerializeField] private Image evidenceImage;
    [SerializeField] private TMP_Text evidenceNameText;
    [SerializeField] private TMP_Text locationText;

    [SerializeField]
    private TMP_Text relatedSuspectText;

    [SerializeField] private TMP_Text descriptionText;

    [Header("선택 전 안내 문구")]
    [SerializeField]
    private string emptyNameText =
        "확인할 증거를 선택하세요.";

    private void Start()
    {
        Clear();
    }

    public void ShowEvidence(EvidenceData evidence)
    {
        if (evidence == null)
        {
            Clear();
            return;
        }

        if (evidenceImage != null)
        {
            evidenceImage.sprite = evidence.evidenceImage;
            evidenceImage.enabled =
                evidence.evidenceImage != null;
        }

        if (evidenceNameText != null)
        {
            evidenceNameText.text = evidence.evidenceName;
        }

        if (locationText != null)
        {
            locationText.text =
                $"발견 장소\n{evidence.discoveredLocation}";
        }

        if (relatedSuspectText != null)
        {
            relatedSuspectText.text =
                GetRelatedSuspectText(evidence);
        }

        if (descriptionText != null)
        {
            descriptionText.text = evidence.description;
        }
    }

    private string GetRelatedSuspectText(EvidenceData evidence)
    {
        if (evidence.relatedSuspects == null ||
            evidence.relatedSuspects.Count == 0)
        {
            return "관련 용의자\n없음";
        }

        StringBuilder builder = new StringBuilder();

        builder.Append("관련 용의자\n");

        bool hasSuspect = false;

        foreach (SuspectProfile suspect in evidence.relatedSuspects)
        {
            if (suspect == null)
            {
                continue;
            }

            if (hasSuspect)
            {
                builder.Append(", ");
            }

            builder.Append(suspect.suspectName);

            hasSuspect = true;
        }

        if (!hasSuspect)
        {
            builder.Append("없음");
        }

        return builder.ToString();
    }

    public void Clear()
    {
        if (evidenceImage != null)
        {
            evidenceImage.sprite = null;
            evidenceImage.enabled = false;
        }

        if (evidenceNameText != null)
        {
            evidenceNameText.text = emptyNameText;
        }

        if (locationText != null)
        {
            locationText.text = string.Empty;
        }

        if (relatedSuspectText != null)
        {
            relatedSuspectText.text = string.Empty;
        }

        if (descriptionText != null)
        {
            descriptionText.text = string.Empty;
        }
    }
}