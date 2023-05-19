using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
public class BattleManager : Singleton<BattleManager>
{
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
                Debug.Log($"當前人數 : {currentPlayerCount}");
            }
        }
    }
    private void Update()
    {
        if (currentPlayerCount <= 1)
        {
            //當前人數剩餘1人時
        
        }
    }

}
