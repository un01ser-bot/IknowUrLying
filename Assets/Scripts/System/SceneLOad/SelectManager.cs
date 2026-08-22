using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    private string nextscenename = "TutorialScene";

    public void MovetoTutorialScene()
    {
        SceneManager.LoadScene(nextscenename);
        
    }


}

