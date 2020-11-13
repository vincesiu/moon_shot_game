using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
