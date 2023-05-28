using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    Animator ani;
    CanvasGroup canvasGroup;
    void Awake()
    {
        ani = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        Actions.GameOverUI += FadeOutAnimation;
        Actions.GameStartUI += FadeInAnimation;
    }
    public void GroupOn()
    {
        canvasGroup.alpha = 1;
    }
    public void GroupOff()
    {
        canvasGroup.alpha = 0;

    }
    public void FadeInAnimation()
    {
        if (ani == null) return;
        ani.SetTrigger("FadeIn");
        Debug.Log("FadeIn");

    }
    public void FadeOutAnimation()
    {
        if (ani == null) return;
        ani.SetTrigger("FadeOut");
        Debug.Log("FadeOut");
    }
    private void OnDisable()
    {
        Actions.GameOverUI -= FadeOutAnimation;
        Actions.GameStartUI -= FadeInAnimation;

    }

}
