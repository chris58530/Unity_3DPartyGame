using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;
public class NetworkPlayerCanvas : NetworkBehaviour
{

    [SerializeField]
    public TMP_Text speedText;
    [SerializeField]
    private TMP_Text playerName;

    [SerializeField]
    public Image AngryBar;
    BattleManager battleManager;
    NetworkPlayerData data;
    void Update()
    {
        transform.LookAt(Camera.main.transform);

    }

    public override void Spawned()
    {
        if (GameManager.Instance.PlayerList.TryGetValue(Object.InputAuthority, out var data))
        {
            playerName.text = data.PlayerName;
            data.PlayerScore = (float)PlayerScore.Score1;
            Debug.Log($"玩家 : {data.PlayerName} 目前分數 : {data.PlayerScore}");
        }
    }
    public override void FixedUpdateNetwork()
    {
        if (battleManager != null)
        {
            if (GameManager.Instance.PlayerList.TryGetValue(Object.InputAuthority, out var data))
            {
                battleManager.PlayerValue[data.CharaterCount].fillAmount = data.PlayerScore / 100;
            }
        }
        else
        {
            battleManager = FindObjectOfType<BattleManager>();
        }
    }





}
