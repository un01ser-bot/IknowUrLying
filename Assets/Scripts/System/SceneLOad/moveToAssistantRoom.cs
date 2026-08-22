using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToAssistantRoom : MonoBehaviour
{
    [SerializeField] private string nextSceneName_1 = "AssistantRoom";
    [SerializeField] private string nextSceneName_2 = "ChatScene";

    public void MoveToAssistantRoom()
    {
        SceneManager.LoadScene(nextSceneName_1);
    }

    public void MoveToChatScene()
    {
        SceneManager.LoadScene(nextSceneName_2);
    }

  
}
