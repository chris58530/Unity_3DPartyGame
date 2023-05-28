using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private NetworkRunner runner = null;
    public NetworkRunner Runner
    {
        get
        {
            if (runner == null)
            {
                runner = gameObject.AddComponent<NetworkRunner>();
                runner.ProvideInput = true;
            }
            return runner;
        }
    }
    public string PlayerName = null;
    public int PlayerCharacter = 0;
    public int PlayerScore = 0;
    public Dictionary<PlayerRef, NetworkPlayerData> PlayerList = new Dictionary<PlayerRef, NetworkPlayerData>();
    [SerializeField]
    private GameObject loadingCanvas;
    public bool ReadyScene = false;

    public event Action OnPlayerListUpdated = null;

    protected override void Awake()
    {
        base.Awake();
        Runner.ProvideInput = true;
        DontDestroyOnLoad(gameObject);
        // SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName("GameEnd"));
    }
   
    private void Update()
    {
        if (ReadyScene)
            loadingCanvas.SetActive(false);
    }
    private bool CheckAllPlayerIsReady()
    {
        if (!Runner.IsServer) return false;

        foreach (var playerData in PlayerList.Values)
        {
            if (!playerData.IsReady) return false;
        }
        foreach (var playerData in PlayerList.Values)
        {
            playerData.IsReady = false;
        }

        return true;
    }
    public void UpdatePlayerList()
    {
        OnPlayerListUpdated?.Invoke();

        if (CheckAllPlayerIsReady())
        {
            NextScene();
        }
    }
    public void SetPlayerNetworkData()
    {
        if (PlayerList.TryGetValue(runner.LocalPlayer, out NetworkPlayerData networkPlayerData))
        {
            networkPlayerData.SetPlayerName_RPC(PlayerName);
            networkPlayerData.SetCharacterCount_RPC(PlayerCharacter);
            networkPlayerData.SetPlayerScore_RPC(PlayerScore);
        }
    }
  
    public void NextScene()
    {
        //自動判定場景切換
        string activeSceneName = SceneManager.GetActiveScene().name;

        foreach (PlayerRef player in PlayerList.Keys)
        {
            if (PlayerList.TryGetValue(player, out NetworkPlayerData data))
            {
                if (data.PlayerScore >= 3)
                {
                    Runner.SetActiveScene("GameEnd");
                    return;
                }
            }
        }
        switch (activeSceneName)
        {
            case "Lobby":
                // Runner.SetActiveScene("GamePlay1");//test
                Runner.SetActiveScene("ReadyScene");
                Debug.Log("Switch to Scene 'ReadyScene'");
                break;

            case "ReadyScene":
                Runner.SetActiveScene("GamePlay0");//test
                // Runner.SetActiveScene("GamePlay0");
                Debug.Log("Switch to Scene 'GamePlay0'");
                break;

            case "GamePlay0":
                Runner.SetActiveScene("GamePlay1");
                Debug.Log("Switch to Scene 'GamePlay1'");
                break;

            case "GamePlay1":
                Runner.SetActiveScene("GamePlay0");
                Debug.Log("Switch to Scene 'GamePlay0'");
                break;

            case "GameEnd":
                Runner.SetActiveScene("ReadyScene");
                Debug.Log("Switch to Scene 'ReadyScene'");
                break;
        }
        // foreach (PlayerRef player in GameManager.Instance.PlayerList.Keys)
        // {
        //     if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData data))
        //     {

        //     }
        // }
    }
    
}


