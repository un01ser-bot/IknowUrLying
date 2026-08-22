using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToSelectScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "selectScene";


    public void moveTonextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
