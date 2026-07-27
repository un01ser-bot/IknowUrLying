using UnityEngine;

[CreateAssetMenu(
    fileName = "Evidence_",
    menuName = "Detective Game/Evidence Data"
)]
public class EvidenceData : ScriptableObject
{
    [Header("저장용 고유 ID")]
    [Tooltip("다른 증거와 절대 겹치면 안 됩니다.")]
    public string evidenceId;

    [Header("증거 정보")]
    public string evidenceName;

    [Tooltip("예: 피해자의 방 책상 아래")]
    public string discoveredLocation;

    [TextArea(4, 10)]
    public string description;

    [Header("증거 이미지")]
    public Sprite evidenceImage;

    [Header("향후 확장용")]
    [TextArea(2, 5)]
    public string developerMemo;
}