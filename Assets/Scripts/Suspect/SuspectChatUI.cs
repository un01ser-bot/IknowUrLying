using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuspectChatUI : MonoBehaviour
{
    [Header("심문 UI")]
    [SerializeField] private GameObject chatPanel;

    [Header("용의자 정보")]
    [SerializeField] private TMP_Text suspectNameText;
    [SerializeField] private TMP_Text suspectAgeText;
    [SerializeField] private TMP_Text relationshipText;
    [SerializeField] private Image suspectImage;

    [Header("채팅")]
    [SerializeField] private TMP_InputField questionInputField;
    [SerializeField] private Transform messageContent;
    [SerializeField] private ChatMessageUI playerMessagePrefab;
    [SerializeField] private ChatMessageUI suspectMessagePrefab;

    [Header("플레이어")]
    [SerializeField] private CapsulePlayerMove playerMove;

    [Header("API")]
    [SerializeField] private EliceChatClient eliceChatClient;

    private SuspectProfile currentProfile;

    private void Start()
    {
        questionInputField.onSubmit.AddListener(
            SendPlayerMessage
        );

        if (chatPanel != null)
        {
            chatPanel.SetActive(false);
        }
    }

    public void OpenChat(SuspectProfile profile)
    {
        if (profile == null)
        {
            Debug.LogWarning("SuspectProfile이 없습니다.");
            return;
        }

        currentProfile = profile;

        suspectNameText.text =
            profile.suspectName;

        suspectAgeText.text =
            $"{profile.age}세";

        relationshipText.text =
            profile.relationshipToVictim;

        if (suspectImage != null)
        {
            suspectImage.sprite = profile.suspectImage;
            suspectImage.enabled = profile.suspectImage != null;
        }

        chatPanel.SetActive(true);

        if (playerMove != null)
        {
            playerMove.DisablePlayerControl();
        }

        questionInputField.text = "";
        questionInputField.Select();
        questionInputField.ActivateInputField();
    }

    public void CloseChat()
    {
        chatPanel.SetActive(false);
        currentProfile = null;

        if (playerMove != null)
        {
            playerMove.EnablePlayerControl();
        }
    }

    private void SendPlayerMessage(string submittedText)
    {
        string message =
            submittedText.Trim();

        if (string.IsNullOrEmpty(message))
        {
            questionInputField.ActivateInputField();
            return;
        }

        if (playerMessagePrefab == null)
        {
            Debug.LogWarning(
                "PlayerMessagePrefab이 연결되지 않았습니다."
            );
            return;
        }

        if (messageContent == null)
        {
            Debug.LogWarning(
                "MessageContent가 연결되지 않았습니다."
            );
            return;
        }

        ChatMessageUI newMessage =
            Instantiate(
                playerMessagePrefab,
                messageContent
            );

        newMessage.SetMessage(message);

        questionInputField.text = "";

        questionInputField.interactable = false;

        if (eliceChatClient == null)
        {
            Debug.LogWarning(
                "EliceChatClient가 연결되지 않았습니다."
            );

            questionInputField.interactable = true;
            questionInputField.Select();
            questionInputField.ActivateInputField();
            return;
        }

        eliceChatClient.SendSuspectMessage(
            currentProfile,
            message,
            OnSuspectResponse
        );

    }

    private void OnSuspectResponse(string response)
    {
        AddSuspectMessage(response);

        questionInputField.interactable = true;
        questionInputField.Select();
        questionInputField.ActivateInputField();
    }

    public void AddSuspectMessage(string message)
    {
        if (suspectMessagePrefab == null)
        {
            Debug.LogWarning("SuspectMessagePrefab이 연결되지 않았습니다.");
            return;
        }

        ChatMessageUI newMessage =
            Instantiate(suspectMessagePrefab, messageContent);
        newMessage.SetMessage(message);

    }


    private void OnDestroy()
    {
        if (questionInputField != null)
        {
            questionInputField.onSubmit.RemoveListener(
                SendPlayerMessage
            );
        }
    }
}