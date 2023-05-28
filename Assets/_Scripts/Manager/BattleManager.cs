using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BattleManager : Singleton<BattleManager>
{
    [Header("目前玩家人數")]
    public int currentPlayerCount;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"BattleManager.OnSceneLoaded 初始化");
        currentPlayerCount = 0;
        GameManager.Instance.Runner.ProvideInput = true;
    }

    public void CheckAllReadyButton()//Ready大廳
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
    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
