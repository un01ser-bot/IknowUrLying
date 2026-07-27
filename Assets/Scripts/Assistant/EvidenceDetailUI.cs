using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceDetailUI : MonoBehaviour
{
    [Header("상세 정보 UI")]
    [SerializeField] private Image evidenceImage;
    [SerializeField] private TMP_Text evidenceNameText;
    [SerializeField] private TMP_Text locationText;
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

        if (descriptionText != null)
        {
            descriptionText.text = evidence.description;
        }
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

        if (descriptionText != null)
        {
            descriptionText.text = string.Empty;
        }
    }
}