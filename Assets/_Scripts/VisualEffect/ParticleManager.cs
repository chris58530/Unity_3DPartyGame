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
        for (int i = 0; i < thisParticle.Length; i++)
        {
            //Actions.StopEffect?.Invoke((EffectType)i);
            thisParticle[i].Stop();
            RPC_StopParticle((EffectType)i);

        }
    }
  
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_PlayParticle(EffectType type)
    {
        thisParticle[(int)type].gameObject.SetActive(true);
        if (!thisParticle[(int)type].isPlaying)
            thisParticle[(int)type].Play();

    }
    [Rpc(RpcSources.All, RpcTargets.All)]

    public void RPC_StopParticle(EffectType type)
    {
        thisParticle[(int)type].gameObject.SetActive(false);

        thisParticle[(int)type].Stop();
    }
    [Rpc(RpcSources.All, RpcTargets.All)]

    public void RPC_StopAllParticle()
    {
        for (int i = 0; i < thisParticle.Length; i++)
        {
            //Actions.StopEffect?.Invoke((EffectType)i);
            thisParticle[i].Stop();
            thisParticle[i].gameObject.SetActive(false);

        }
    }
}
