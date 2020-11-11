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


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape) && pauseOverlayStatus == PauseOverlayStatus.NotLoading)
        {
            Debug.Log("Starting to async load pause overlay");
            StartCoroutine(LoadPauseScene());
            pauseOverlayStatus = PauseOverlayStatus.Loading;
        }
        */
    }

    IEnumerator LoadGameOverScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);

        while (!operation.isDone)
        {
            yield return null;
        }
        
    }
}