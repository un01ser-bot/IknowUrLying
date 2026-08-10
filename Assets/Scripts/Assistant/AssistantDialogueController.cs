using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssistantDialogueController : MonoBehaviour
{
    [Header("대화 UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [Header("선택지")]
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button noteButton;
    [SerializeField] private Button hintButton;
    [SerializeField] private Button cancelButton;

    [Header("대사")]
    [SerializeField]
    private string enterDialogue =
        "탐정님, 오셨어요?";

    [SerializeField]
    private string clickedDialogue =
        "뭘 도와드릴까요?";

    [Header("선택 후 임시 대사")]
    [SerializeField]
    private string noteDialogue =
        "정리해둔 수사 노트를 보여드릴게요.";

    [SerializeField]
    private string hintDialogue =
        "어디부터 살펴보면 좋을지 같이 생각해볼까요?";

    [SerializeField]
    private string cancelDialogue =
        "알겠어요. 필요하면 다시 불러주세요.";

    [Header("타자기 효과")]
    [SerializeField]
    private float typingSpeed = 0.04f;

    private Coroutine typingCoroutine;

    private void Start()
    {
        SetupButtons();

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        if (choicePanel != null)
        {
            choicePanel.SetActive(false);
        }

        TypeDialogue(enterDialogue);
    }

    private void SetupButtons()
    {
        if (noteButton != null)
        {
            noteButton.onClick.RemoveAllListeners();
            noteButton.onClick.AddListener(OnNoteSelected);
        }

        if (hintButton != null)
        {
            hintButton.onClick.RemoveAllListeners();
            hintButton.onClick.AddListener(OnHintSelected);
        }

        if (cancelButton != null)
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(OnCancelSelected);
        }
    }

    public void OnAssistantClicked()
    {
        HideChoices();

        TypeDialogue(
            clickedDialogue,
            ShowChoices
        );
    }

    private void OnNoteSelected()
    {
        HideChoices();

        TypeDialogue(noteDialogue);

        Debug.Log("수사 노트 보기 선택");
    }

    private void OnHintSelected()
    {
        HideChoices();

        TypeDialogue(hintDialogue);

        Debug.Log("힌트 받기 선택");
    }

    private void OnCancelSelected()
    {
        HideChoices();

        TypeDialogue(cancelDialogue);
    }

    private void ShowChoices()
    {
        if (choicePanel != null)
        {
            choicePanel.SetActive(true);
        }
    }

    private void HideChoices()
    {
        if (choicePanel != null)
        {
            choicePanel.SetActive(false);
        }
    }

    private void TypeDialogue(
        string text,
        System.Action onComplete = null
    )
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine =
            StartCoroutine(
                TypeDialogueCoroutine(
                    text,
                    onComplete
                )
            );
    }

    private IEnumerator TypeDialogueCoroutine(
        string text,
        System.Action onComplete
    )
    {
        if (dialogueText == null)
        {
            yield break;
        }

        dialogueText.text = "";

        foreach (char letter in text)
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(
                typingSpeed
            );
        }

        typingCoroutine = null;

        onComplete?.Invoke();
    }
}