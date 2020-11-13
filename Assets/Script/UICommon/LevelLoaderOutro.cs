using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderOutro : MonoBehaviour
{
    // Note: this is NOT synchronized to how long the Animation Clip takes to run the actual intro. Please synchronize manually
    int OUTRO_DURATION_SECS = 1;
    // Used to deactivate the UI elements
    public GameObject levelLoaderOutroCanvasHandle;
    // Used to set the trigger to start the animation
    public Animator levelLoaderOutroAnimator;
    // Start is called before the first frame update
    
    void Start()
    {
        levelLoaderOutroCanvasHandle.SetActive(false);
    }

    // Why does this exist? Why does unity not let us apply functions with return values through the UI? The world may never know. Anyways, we need this in case someone wants to attach this outro to a button for debugging purposes
    public void RunDebug()
    {
        Run();
    }

    public int Run()
    {
        Debug.Log("Starting level loader outro");
        levelLoaderOutroCanvasHandle.SetActive(true);
        levelLoaderOutroAnimator.SetTrigger("LevelLoaderOutroTrigger");
        return OUTRO_DURATION_SECS;
    }
}
