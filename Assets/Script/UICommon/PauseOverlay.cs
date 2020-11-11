using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseOverlay : MonoBehaviour
{
    public GameObject pauseOverlay;

    void Start()
    {
        pauseOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseOverlay.activeSelf)
        {
            Debug.Log("Starting to async load pause overlay");
            pauseOverlay.SetActive(true);
        }
    }

    public void ClickButtonExit()
    {
        Debug.Log("Exit button clicked from Pause Overlay");
        Application.Quit();
    }

    public void ClickButtonReturnToGame()
    {
        Debug.Log("Return to game button clicked from the Pause Overlay");
        pauseOverlay.SetActive(false);
    }

    public void ClickButtonReturnToMenu()
    {
        Debug.Log("Return to menu button clicked from the Pause Overlay");
        SceneManager.LoadScene("Title");
    }
}
