using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void ClickButtonReturn()
    {
        Debug.Log("Return button clicked from the Pause Overlay");
        pauseOverlay.SetActive(false);
    }
}
