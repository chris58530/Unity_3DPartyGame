using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
public class BattleManager : Singleton<BattleManager>
{
    [Header("目前玩家人數")]
    public int currentPlayerCount;
    [SerializeField]
    private BattleCanvas battleCanvas;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

    }
    private void Start()
    {
        foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
        {
            if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
            {
                data.IsDead = false;
                currentPlayerCount += 1;
                Debug.Log($"{this} 初始化...");
                Debug.Log($"當前遊戲人數 : {currentPlayerCount}");
            }
        }
    }
  
    public void CheckAllReadyButton()
    {
        int currentReayBt = 0;
        ReadyButton[] ready = FindObjectsOfType<ReadyButton>();
        foreach (ReadyButton readyButton in ready)
        {
            if (readyButton.isReady)
            {
                currentReayBt += 1;
                if (currentReayBt == currentPlayerCount)
                {
                    GameManager.Instance.NextScene();
                    break;
                }
            }
        }
    }
}
