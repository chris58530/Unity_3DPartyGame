using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkAnimator : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnAnimationChanged))]
    public string PlayAnimationString { get; set; } = null;
    public bool IsFinish(float time)
    {
        // Debug.Log($"StateTime : {time} Animation : {ani.GetCurrentAnimatorClipInfo(0).Length}");
        return time >= ani.GetCurrentAnimatorStateInfo(0).length;
    }
    public Animator ani;
    public override void Spawned()
    {
        ani = GetComponentInChildren<Animator>();
    }
    private static void OnAnimationChanged(Changed<NetworkAnimator> changed)
    {
        //     if (changed.Behaviour.ani != null)
        //         changed.Behaviour.ani.Play(changed.Behaviour.PlayAnimationString);
        if (changed.Behaviour.ani != null)
            changed.Behaviour.ani.SetTrigger(changed.Behaviour.PlayAnimationString);


    }}
