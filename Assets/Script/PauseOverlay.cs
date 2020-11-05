using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseOverlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Debug.Log("meow");
        }
        */
    }

    public void ClickButtonExit()
    {
        Debug.Log("Exit button clicked from Pause Overlay");
        Application.Quit();
    }

    public void ClickButtonReturn()
    {
        Debug.Log("Return button clicked from the Pause Overlay");
        Unload();
    }

    void Unload()
    {
        SceneManager.UnloadSceneAsync("PauseOverlay");
    }
}
