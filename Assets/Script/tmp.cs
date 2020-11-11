using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tmp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator meow()
    {
        Debug.Log("meow woof meow");
        SceneManager.LoadSceneAsync("Title");
        yield return null;
    }
}
