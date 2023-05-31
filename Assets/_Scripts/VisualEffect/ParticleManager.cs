using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class ParticleManager : NetworkBehaviour
{

    [SerializeField]
    private ParticleSystem[] thisParticle;
    public override void Spawned()
    {
        Debug.Log("所有特效stop()");
        for (int i = 0; i < thisParticle.Length; i++)
        {
            //Actions.StopEffect?.Invoke((EffectType)i);
            thisParticle[i].Stop();
            RPC_StopParticle((EffectType)i);

        }
    }
    // void OnEnable()
    // {
    //     Actions.PlayEffect += RPC_PlayParticle;
    //     Actions.StopEffect += RPC_StopParticle;
    // }
    // void OnDisable()
    // {
    //     Actions.PlayEffect -= RPC_PlayParticle;
    //     Actions.StopEffect -= RPC_StopParticle;
    // }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayParticle(EffectType type)
    {
        thisParticle[(int)type].gameObject.SetActive(true);
        thisParticle[(int)type].Play();
        
    }
    [Rpc(RpcSources.All, RpcTargets.All)]

    public void RPC_StopParticle(EffectType type)
    {
        thisParticle[(int)type].gameObject.SetActive(false);
        thisParticle[(int)type].Stop();
    }
}
