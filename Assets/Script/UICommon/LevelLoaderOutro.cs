using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderOutro : MonoBehaviour
{
    Animator levelLoaderOutroAnimator;

    // Start is called before the first frame update
    void Start()
    {
        var levelLoaderOutroImage = GameObject.Find("LevelLoaderOutro/Canvas/LevelLoaderOutroImage");
        levelLoaderOutroAnimator = levelLoaderOutroImage.GetComponent<Animator>();
    }
    
    public void StartLevelLoaderOutro()
    {
        Debug.Log("Starting level loader outro");
        levelLoaderOutroAnimator.SetTrigger("StartLevelLoaderOutro");
    }
}
