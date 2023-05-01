using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;

public class BasicSpawner : Singleton<BasicSpawner>, INetworkRunnerCallbacks
{
    [SerializeField]
    private NetworkRunner networkRunner;
    [SerializeField]
    private NetworkPrefabRef[] playerPrefab;
    int playerCount;
    NetworkPlayerInput playerInput;

    public Dictionary<PlayerRef, NetworkObject> playerList = new Dictionary<PlayerRef, NetworkObject>();
    void Start()
    {
        playerCount = 0;
        //自動適配房間，沒房間就開房，有就加入
        StartGame(GameMode.AutoHostOrClient);
    }
    async void StartGame(GameMode mode)
    {
        //本地玩家可以提供input給server
        networkRunner.ProvideInput = true;

        await networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "Fusion Room",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (playerInput == null && NetworkPlayer.Local != null)
        {
            playerInput = NetworkPlayer.Local.GetComponent<NetworkPlayerInput>();
            Debug.Log("didnt get input");

        }
        if (playerInput != null)
        {
            Debug.Log("get input");
            input.Set(playerInput.GetNetworkInput());
        }

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Vector3 spawnPosition = new Vector3(0, 2, 0);
            NetworkObject networkObjectPlayer = runner.Spawn(playerPrefab[playerCount], spawnPosition, Quaternion.identity, player);
            playerList.Add(player, networkObjectPlayer);
            Debug.Log("playerid:" + player.PlayerId);
        }
        // if (runner.IsServer)
        // {
        //     runner.Spawn(playerPrefab[playerCount], spawnPosition, Quaternion.identity, player);
        // }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (playerList.TryGetValue(player, out NetworkObject networkObject))
        {
            //Runner.Despawn 與 Unity Destroy 相通
            runner.Despawn(networkObject);
            playerList.Remove(player);
        }
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

        Debug.Log("OnConnectFailed");

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }


    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }
}
