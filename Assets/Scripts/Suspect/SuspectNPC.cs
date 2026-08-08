using UnityEngine;

public class SuspectNPC : MonoBehaviour
{
    [Header("이 용의자의 정보")]
    [SerializeField] private SuspectProfile suspectProfile;

    public SuspectProfile Profile => suspectProfile;
}