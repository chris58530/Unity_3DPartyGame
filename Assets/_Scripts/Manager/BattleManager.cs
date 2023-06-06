using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using Cinemachine;

public class BattleManager : Singleton<BattleManager>
{
    [Header("目前玩家人數")]
    public int currentPlayerCount;
    float time;
    bool canSwitch = false;
    private CinemachineTargetGroup camGroup;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        camGroup = FindObjectOfType<CinemachineTargetGroup>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void FixedUpdate()
    {
        if (canSwitch)
        {
            time += Time.deltaTime;
            if (time > 2)
            {
                GameManager.Instance.NextScene();
                canSwitch = false;
                time = 0;
                return;
            }

        }
    }
    void Update()
    {
        foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
        {
            if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
            {
                if (data.IsDead)
                {
                    NetworkObject obj = GameManager.Instance.Runner.GetPlayerObject(player);

                    if (camGroup != null)
                    {
                        camGroup.RemoveMember(this.gameObject.transform);
                    }
                    return;
                }
            }
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentPlayerCount = 0;
        GameManager.Instance.Runner.ProvideInput = true;
        if (SceneManager.GetActiveScene().name != "ReadyScene")
        {
            Actions.GameStartUI?.Invoke();
        }
    }

    public void CheckAllReadyButton()//Ready大廳
    {
        if (canSwitch) return;
        int currentReayBt = 0;
        ReadyButton[] ready = FindObjectsOfType<ReadyButton>();
        foreach (ReadyButton readyButton in ready)
        {
            if (readyButton.isReady)
            {
                currentReayBt += 1;
                if (currentReayBt == currentPlayerCount)
                {
                    Actions.GameOverUI?.Invoke();
                    canSwitch = true;
                    return;
                }
            }
        }
    }
    public void CheckAllPlayerDie()
    {
        int dieCount = 0;
        foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
        {
            if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
            {
                if (data.IsDead)
                {
                    dieCount += 1;
                    if (dieCount >= currentPlayerCount)
                    {

                        currentPlayerCount -= dieCount;
                    }
                }

            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
