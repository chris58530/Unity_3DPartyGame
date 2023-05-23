using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class BattleCanvas : NetworkBehaviour
{
    [Header("本局遊戲結束聚焦剩餘玩家時間")]
    private float focusTime = 4;
    [Networked]
    private TickTimer focusTimer { get; set; }
    [Networked(OnChanged = nameof(OnEndGame))]
    public NetworkBool IsEndGame { get; set; }
    [SerializeField]

    public Image[] PlayerValue;
    // [SerializeField]
    // public Image[] PlayerIcon;

    [SerializeField]
    private float battleTime;

    [SerializeField]
    private TMP_Text timeText;

    [Networked, HideInInspector]
    public int PlayerScore { get; set; }

    [Networked(OnChanged = nameof(OnBattleTimeChange)), HideInInspector]
    public float BattleTime { get; set; }

    private CinemachineVirtualCamera[] virtualCamera;
    CinemachineExtension d;

    public override void Spawned()
    {
        IsEndGame = false;

        BattleTime = battleTime;
        focusTimer = TickTimer.None;

    
    }
    public override void FixedUpdateNetwork()
    {
        
        // if (BattleManager.Instance.currentPlayerCount <= 0)
        // {
        //     IsEndGame = true;
        // }
        if (BattleTime > 0)
            BattleTime -= Runner.DeltaTime;

        if (focusTimer.Expired(Runner))//本回合結束時，倒數計時後切換場景
        {
            GameManager.Instance.NextScene();
        }
    }
    public void SetPlayerData(int playerCount, float score)
    {
        PlayerValue[playerCount].fillAmount = score / 3;
    }
    private static void OnBattleTimeChange(Changed<BattleCanvas> changed)
    {
        changed.Behaviour.timeText.text = Mathf.RoundToInt(changed.Behaviour.BattleTime).ToString();
    }
    private static void OnEndGame(Changed<BattleCanvas> changed)
    {
        if (changed.Behaviour.IsEndGame)
        {
            Debug.Log("OnEndGame");

            foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
            {
                if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
                {
                    if (!data.IsDead)
                    {
                        data.PlayerScore += 1;
                        Debug.Log($"本局贏家 : {data.PlayerName}");
                        Debug.Log($"分數 : {data.PlayerScore}");
                    }
                }
            }
            changed.Behaviour.virtualCamera = FindObjectsOfType<CinemachineVirtualCamera>();
            foreach (CinemachineVirtualCamera cam in changed.Behaviour.virtualCamera)
            {
                cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 2;
            }
            changed.Behaviour.focusTimer = TickTimer.CreateFromSeconds(changed.Behaviour.Runner, changed.Behaviour.focusTime);
        }
    }

}
