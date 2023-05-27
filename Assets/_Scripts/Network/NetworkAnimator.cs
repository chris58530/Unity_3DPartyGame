using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkAnimator : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnAnimationChanged))]
    public string PlayAnimationString { get; set; } = null;


    public NetworkBool IsFinish;
    Animator ani;

    [Networked]
    private TickTimer aniTimer { get; set; }
    public override void Spawned()
    {
        ani = GetComponentInChildren<Animator>();
        IsFinish = false;

    }
    public override void FixedUpdateNetwork()
    {
        if (aniTimer.ExpiredOrNotRunning(Runner))
        {
            IsFinish = true;
        }
        else IsFinish = false;
        Debug.Log(IsFinish);
    }

    private static void OnAnimationChanged(Changed<NetworkAnimator> changed)
    {
        if (changed.Behaviour.ani != null)
            changed.Behaviour.ani.Play(changed.Behaviour.PlayAnimationString);
        changed.Behaviour.aniTimer = TickTimer.CreateFromSeconds(changed.Behaviour.Runner,
        changed.Behaviour.ani.GetCurrentAnimatorStateInfo(0).length);
        Debug.Log(changed.Behaviour.ani.GetCurrentAnimatorStateInfo(0).length);
        changed.Behaviour.IsFinish = false;
    }
    private static void OnAnimationTime(Changed<NetworkAnimator> changed)
    {
        // changed.Behaviour.IsFinish = changed.Behaviour.aniTime >=
        // changed.Behaviour.ani.GetCurrentAnimatorStateInfo(0).length ? true : false;


    }
}
