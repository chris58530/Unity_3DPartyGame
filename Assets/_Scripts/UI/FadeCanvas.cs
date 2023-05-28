using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    Animator ani;
    void Awake()
    {
        ani = GetComponent<Animator>();
    }
  
    private void OnEnable()
    {
        Actions.GameOverUI += FadeOutAnimation;
        Actions.GameStartUI += FadeInAnimation;

    }
    public void FadeInAnimation()
    {
        ani.SetTrigger("FadeIn");
    }
    public void FadeOutAnimation()
    {
        ani.SetTrigger("FadeOut");

    }
    private void OnDisable()
    {
        Actions.GameOverUI -= FadeOutAnimation;
        Actions.GameStartUI -= FadeInAnimation;

    }

}
