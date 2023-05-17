using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BattleManager : NetworkBehaviour
{
    [SerializeField]
    private float battleTime;

    [Networked(OnChanged = nameof(OnBattleTimeChange)),HideInInspector]
    public float BattleTime { get; set; }

    private BattleCanvas battleCanvas;
    private NetworkPlayerData playerData;
    private void Awake()
    {
        battleCanvas = FindObjectOfType<BattleCanvas>();
    }

    public override void Spawned()
    {
        BattleTime = battleTime;
       


    }
    public override void FixedUpdateNetwork()
    {
        if(!Object.HasStateAuthority)return;
        if(BattleTime>0)
        BattleTime -= Runner.DeltaTime;
    }
    private static void OnBattleTimeChange(Changed<BattleManager> changed)
    {
        changed.Behaviour.battleCanvas.timeText.text = Mathf.RoundToInt(changed.Behaviour.BattleTime).ToString();

    }

}
