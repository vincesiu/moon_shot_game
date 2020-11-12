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

    public GameObject levelLoaderIntroObject;
    // public BoundsInt bounds = new BoundsInt(new Vector3Int(0,0,0), sizeof: new Vector3Int(100,100,0);

    void Start()
    {
        // Will probably need to kick off a coroutine that turns on controls after the intro duration
        LevelLoaderIntro levelLoaderIntroScriptHandle = levelLoaderIntroObject.GetComponent<LevelLoaderIntro>();
        int intro_duration = levelLoaderIntroScriptHandle.Run();
        
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