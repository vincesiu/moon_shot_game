using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


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

    private int tempDeleteMe;

    void Start()
    {
        // Will probably need to kick off a coroutine that turns on controls after the intro duration
        if (levelLoaderIntroObject != null) {
            LevelLoaderIntro levelLoaderIntroScriptHandle = levelLoaderIntroObject.GetComponent<LevelLoaderIntro>();
            int intro_duration = levelLoaderIntroScriptHandle.Run();
        }

        Debug.Log("meow");

        if (EventManager.current == null)
        {
            throw new Exception("Could not find an EventManager in the current scene, cowardly refusing to proceed");
        }
        EventManager.current.onCharacterDeathEvent += LoadGameOverScene;
        
        EventManager.current.EnableUserInput(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EventManager.current.CharacterDamageEvent(1);
        }
    }

    private void LoadGameOverScene()
    {
        StartCoroutine(GenLoadGameOverScene());
    }

    IEnumerator GenLoadGameOverScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}