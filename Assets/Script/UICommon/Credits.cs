using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCard : MonoBehaviour
{
    private bool canGoHome;

    void Start()
    {
        canGoHome = false;
        StartCoroutine(LetMeLeave());
    }

    IEnumerator LetMeLeave()
    {
        yield return new WaitForSeconds(3.0f);
        canGoHome = true;
    }


    void FixedUpdate()
    {
        if (Input.anyKey && canGoHome)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
