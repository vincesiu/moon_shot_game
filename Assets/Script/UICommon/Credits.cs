using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCard : MonoBehaviour
{

    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
