using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem[] thisParticle;
    void Start()
    {
        for (int i =0 ; i < thisParticle.Length; i++)
        {
            thisParticle[i].Stop();
        }

    }
    void OnEnable()
    {
        Actions.PlayEffect += PlayParticle;
        Actions.StopEffect += StopParticle;
    }
    void OnDisable()
    {
        Actions.PlayEffect -= PlayParticle;
        Actions.StopEffect -= StopParticle;

    }

    public void PlayParticle(Transform trans, EffectType type)
    {
        thisParticle[(int)type].gameObject.transform.position = trans.position;
        thisParticle[(int)type].Play();
        Debug.Log($"{type} 播放中......");
    }
    public void StopParticle( EffectType type)
    {
        thisParticle[(int)type].Stop();
        Debug.Log($"{type} 停止......");
    }
}
