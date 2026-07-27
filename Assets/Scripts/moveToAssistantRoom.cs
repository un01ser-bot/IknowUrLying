using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToAssistantRoom : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "AssistantRoom";

    public void MoveToAssistantRoom()
    {
        SceneManager.LoadScene(nextSceneName);
    }


  
}
