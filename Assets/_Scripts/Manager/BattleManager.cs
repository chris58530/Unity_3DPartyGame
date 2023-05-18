using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;
public class BattleManager : NetworkBehaviour
{
    public static BattleManager instance;

    [SerializeField]
    private float battleTime;

    [Networked(OnChanged = nameof(OnBattleTimeChange)), HideInInspector]
    public float BattleTime { get; set; }


    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    public Image[] PlayerValue;

    [Networked, HideInInspector]
    public int PlayerScore { get; set; }
    [Networked(OnChanged = nameof(OnPlayerCountChange))]
    public int currentPlayerCount { get; set; }
    public override void Spawned()
    {
        instance = this;

        BattleTime = battleTime;
        currentPlayerCount = GameManager.Instance.PlayerCount;
        Debug.Log(currentPlayerCount);
    }
    public override void FixedUpdateNetwork()
    {
        if (BattleTime > 0)
            BattleTime -= Runner.DeltaTime;

    }
    private static void OnBattleTimeChange(Changed<BattleManager> changed)
    {
        changed.Behaviour.timeText.text = Mathf.RoundToInt(changed.Behaviour.BattleTime).ToString();
    }
    private static void OnPlayerCountChange(Changed<BattleManager> changed)
    {
        // if(!changed.Behaviour.Runner.IsServer)return;
        if (changed.Behaviour.currentPlayerCount <= 0)
        {
            Debug.Log(changed.Behaviour.currentPlayerCount + "changed scene");

            changed.Behaviour.Runner.SetActiveScene("GamePlay");
        }
    }



}
