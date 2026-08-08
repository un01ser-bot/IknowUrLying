using TMPro;
using UnityEngine;

public class ChatMessageUI : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;

    public void SetMessage(string message)
    {
        messageText.text = message;
    }
}