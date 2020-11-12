using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderIntro : MonoBehaviour
{
    // Note: if you import this into a random scene and don't call the run function, that is fine. We will have the intro card placed over the main game camera forever.

    // Note: this is NOT synchronized to how long the Animation Clip takes to run the actual intro. Please synchronize manually
    int INTRO_DURATION_SECS = 1;
    // Used to deactivate the UI elements
    public GameObject levelLoaderIntroCanvasHandle;
    // Used to set the trigger to start the animation
    public Animator levelLoaderIntroAnimator;

    public int Run()
    {
        Debug.Log("Run level loader intro");
        StartCoroutine(DeactivateLevelLoaderIntro());
        return INTRO_DURATION_SECS;
    }

    IEnumerator DeactivateLevelLoaderIntro()
    {
        levelLoaderIntroAnimator.SetTrigger("LevelLoaderIntroTrigger");
        yield return new WaitForSeconds(INTRO_DURATION_SECS);
        Debug.Log("Deactivating level loader intro");
        levelLoaderIntroCanvasHandle.SetActive(false);
    }

}
