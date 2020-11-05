using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void onExitClick()
    {
        Debug.Log("Exit button clicked");
    }

    public void onStartClick(int targetScene)
    {
        Debug.Log("Start button clicked");
        Debug.Log(string.Format("Switching to scene {0}", targetScene));
        SceneManager.LoadScene(targetScene);
    }
}
