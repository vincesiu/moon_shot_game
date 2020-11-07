using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    /*
     * 
     * Documentation on AsyncOperation 
     * https://docs.unity3d.com/ScriptReference/AsyncOperation.html
     *
     * 
     */

    // vince: I want this eventually to be Unloaded, Loading, and Loaded. For now, this works, will fix later.
    enum PauseOverlayStatus
    {
        NotLoading,
        Loading,
    }

    PauseOverlayStatus pauseOverlayStatus;


    void Start()
    {
        pauseOverlayStatus = PauseOverlayStatus.NotLoading;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseOverlayStatus == PauseOverlayStatus.NotLoading)
        {
            Debug.Log("Starting to async load pause overlay");
            StartCoroutine(LoadPauseScene());
            pauseOverlayStatus = PauseOverlayStatus.Loading;
        }
    }

    IEnumerator LoadPauseScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("PauseOverlay", LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }

        pauseOverlayStatus = PauseOverlayStatus.NotLoading;
    }
}