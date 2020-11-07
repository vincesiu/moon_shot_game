using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    bool PauseScreenLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseScreenLoaded)
            {
                Debug.Log("Escape pressed");
                StartCoroutine(LoadScene("PauseOverlay"));
            }
        }
    }

    IEnumerator LoadScene(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("PauseOverlay", LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }

        PauseScreenLoaded = true;
    }
}