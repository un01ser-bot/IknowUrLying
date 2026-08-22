using UnityEngine;

public class EvidencePanelController : MonoBehaviour
{
    [Header("증거 확인 패널")]
    [SerializeField] private GameObject evidencePanel;

    private void Start()
    {
        if (evidencePanel != null)
        {
            evidencePanel.SetActive(false);
        }
    }

    public void OpenPanel()
    {
        if (evidencePanel == null)
        {
            Debug.LogWarning("Evidence Panel이 연결되지 않았습니다.");
            return;
        }

        evidencePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        if (evidencePanel == null)
        {
            return;
        }

        evidencePanel.SetActive(false);
    }
}