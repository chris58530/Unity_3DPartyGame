using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSound : MonoBehaviour
{
    AudioSource src;
    void Awake()
    {
        src = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        StartCoroutine(MusicPlay());
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "ReadyScene")
        {
            Destroy(this.gameObject);
        }

    }
    IEnumerator MusicPlay()
    {

        yield return new WaitForSeconds(3.8f);
        src.Play();
        yield return null;
    }
}
