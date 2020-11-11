using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderIntro : MonoBehaviour
{
    GameObject levelLoaderIntroHandle;
    
    // Start is called before the first frame update
    void Start()
    {
        levelLoaderIntroHandle = GameObject.Find("LevelLoaderIntro");
        StartCoroutine(DeactivateLevelLoaderIntro());
    }

    IEnumerator DeactivateLevelLoaderIntro()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Deactivating level loader intro");
        levelLoaderIntroHandle.SetActive(false);
    }

}
