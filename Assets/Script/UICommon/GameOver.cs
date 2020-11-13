﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnClickRetry()
    {
        SceneManager.LoadScene("dude testing");
    }

    public void OnClickBackToMenu()
    {
        SceneManager.LoadSceneAsync("Title");
    }
}
