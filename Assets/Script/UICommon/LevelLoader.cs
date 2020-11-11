using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;

    public bool doIntroTransition = true;
    public bool doOutroTransition = true;

    public float outroTransitionLengthSecs = 1.0F;
    public float introTransitionLengthSecs = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        if (doIntroTransition)
        {
            animator.SetTrigger("DoIntroTransition");
        } else
        {
            animator.SetTrigger("SkipIntroTransition");
        }
    }

    public void DoOutroTransition()
    {
        StartCoroutine(GenDoOutroTransition());
    }

    IEnumerator GenDoOutroTransition()
    {
        animator.SetTrigger("DoOutroTransition");
        yield return new WaitForSeconds(outroTransitionLengthSecs);
    }
}
