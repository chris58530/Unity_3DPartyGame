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
            Actions.StopEffect?.Invoke((EffectType)i);
        }

    }
    void OnEnable()
    {
        Actions.PlayEffect += RPC_PlayParticle;
        Actions.StopEffect += RPC_StopParticle;
    }
    void OnDisable()
    {
        Actions.PlayEffect -= RPC_PlayParticle;
        Actions.StopEffect -= RPC_StopParticle;
    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_PlayParticle(EffectType type)
    {
        thisParticle[(int)type].Play();
        Debug.Log($"{type} 播放");
    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]

    public void RPC_StopParticle(EffectType type)
    {
        thisParticle[(int)type].Stop();
        Debug.Log($"{type} 停止......");
    }
}
