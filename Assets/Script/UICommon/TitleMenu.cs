using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    bool loadingGame;

    public void onExitClick()
    {
        Debug.Log("Exit button clicked");
        Application.Quit();
    }

    public void onStartClick(int targetScene)
    {
        Debug.Log("Start button clicked");
        Debug.Log(string.Format("Switching to scene {0}", "dude testing"));
        GameObject startButton = GameObject.Find("StartButton");
        Button btn = startButton.GetComponent<Button>();
        btn.interactable = false;
        StartCoroutine(StartGame(targetScene));

    }

    IEnumerator StartGame(int targetScene)
    {
        AsyncOperation handle = SceneManager.LoadSceneAsync(targetScene);
        handle.allowSceneActivation = false;
        yield return new WaitForSeconds(2);
        handle.allowSceneActivation = true;

    }
}
