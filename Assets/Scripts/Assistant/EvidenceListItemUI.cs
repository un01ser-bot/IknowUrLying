using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceListItemUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image evidenceImage;
    [SerializeField] private TMP_Text evidenceNameText;
    [SerializeField] private Button button;

    private EvidenceData evidenceData;

    public void Setup(
        EvidenceData data,
        Action<EvidenceData> onSelected
    )
    {
        evidenceData = data;

        if (evidenceData == null)
        {
            return;
        }

        if (evidenceNameText != null)
        {
            evidenceNameText.text = evidenceData.evidenceName;
        }

        if (evidenceImage != null)
        {
            evidenceImage.sprite = evidenceData.evidenceImage;
            evidenceImage.enabled =
                evidenceData.evidenceImage != null;
        }

        if (button == null)
        {
            button = GetComponent<Button>();
        }

        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() =>
        {
            onSelected?.Invoke(evidenceData);
        });
    }
}