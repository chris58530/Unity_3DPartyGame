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

    [Networked]
    public int currentPlayerCount { get; set; }

    [Networked(OnChanged =nameof(OnEndGame))]
    public NetworkBool IsEndGame{ get; set; }

    public override void Spawned()
    {
        instance = this;

        BattleTime = battleTime;
        currentPlayerCount = GameManager.Instance.PlayerCount;

        IsEndGame = false;

        foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
        {
            if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
            {
                data.IsDead = false;
                currentPlayerCount += 1;
                Debug.Log($"{this} 初始化...");
                Debug.Log($"當前人數 : {currentPlayerCount}");
            }
        }

    }
    public override void FixedUpdateNetwork()
    {
        if (BattleTime > 0)
            BattleTime -= Runner.DeltaTime;

        /*foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
        {
            if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
            {
                data.IsDead = false;
            }
        }*/

        if(currentPlayerCount <= 1)
        {
            IsEndGame = true;
        }
    }
    private static void OnBattleTimeChange(Changed<BattleManager> changed)
    {
        changed.Behaviour.timeText.text = Mathf.RoundToInt(changed.Behaviour.BattleTime).ToString();
    }
    private static void OnEndGame(Changed<BattleManager> changed)
    {
        if (changed.Behaviour.IsEndGame)
        {
            Debug.Log($"轉換場景");

            foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
            {
                if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
                {
                    if (!data.IsDead)
                    {
                        Debug.Log($"本局贏家 : {data.PlayerName}");
                        data.PlayerScore += 100;
                        Debug.Log($"分數 : {data.PlayerScore}");

                    }
                }
            }
            changed.Behaviour.Runner.SetActiveScene("GamePlay");
        }
    }
}
