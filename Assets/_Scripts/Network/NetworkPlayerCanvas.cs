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
    BattleCanvas battleCanvas;
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
            // data.PlayerScore = (float)PlayerScore.Score1;
            Debug.Log($"玩家 : {data.PlayerName} 目前分數 : {data.PlayerScore}");
            data.IsDead = false;
        }
    }
    public override void FixedUpdateNetwork()
    {
        if (battleCanvas != null)
        {
            if (GameManager.Instance.PlayerList.TryGetValue(Object.InputAuthority, out var data))
            {
                battleCanvas.SetPlayerData(data.CharaterCount,data.PlayerScore);
            }
        }
        else
        {
            battleCanvas = FindObjectOfType<BattleCanvas>();
        }
    }





}
