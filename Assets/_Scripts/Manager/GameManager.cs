using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

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
    public int PlayerCharacter=0;
    public Dictionary<PlayerRef, NetworkPlayerData> PlayerList = new Dictionary<PlayerRef, NetworkPlayerData>();

    public event Action OnPlayerListUpdated = null;

    protected override void Awake()
    {
        base.Awake();
        Runner.ProvideInput = true;
        DontDestroyOnLoad(gameObject);
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
            Runner.SetActiveScene("GamePlay");
        }
    }

    public void SetPlayerNetworkData()
    {
        if (PlayerList.TryGetValue(runner.LocalPlayer, out NetworkPlayerData networkPlayerData))
        {
            networkPlayerData.SetPlayerName_RPC(PlayerName);
            networkPlayerData.SetCharacterCount_RPC(PlayerCharacter);
        }
    }
}


