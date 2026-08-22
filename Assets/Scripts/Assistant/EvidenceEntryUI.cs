using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceEntryUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image evidenceImage;
    [SerializeField] private TMP_Text evidenceNameText;
    [SerializeField] private TMP_Text locationText;
    [SerializeField] private TMP_Text descriptionText;

    public void SetEvidence(EvidenceData evidence)
    {
        if (evidence == null)
        {
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
                $"발견 장소: {evidence.discoveredLocation}";
        }

        if (descriptionText != null)
        {
            descriptionText.text = evidence.description;
        }
    }
}