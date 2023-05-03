using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    private GameManager gameManager;
    private NetworkRunner networkRunner = null;
    [SerializeField]
    private NetworkObject playerPrefab = null;


    NetworkPlayerInput playerInput;

    public Dictionary<PlayerRef, NetworkObject> playerList = new Dictionary<PlayerRef, NetworkObject>();
    void Start()
    {
        gameManager= GameManager.Instance;
        networkRunner = gameManager.Runner;
        networkRunner.AddCallbacks(this);
        SpawnAllPlayers();
    }
    private void SpawnAllPlayers()
    {
        foreach (var player in gameManager.PlayerList.Keys)
        {
            Vector3 spawnPosition = Vector3.zero;
            NetworkObject networkPlayerObject = networkRunner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);

            networkRunner.SetPlayerObject(player, networkPlayerObject);

            playerList.Add(player, networkPlayerObject);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        Debug.Log(xInput + yInput);
        input.Set(data);
    }


    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {

        Vector3 spawnPosition = Vector3.zero;
        NetworkObject networkPlayerObject = runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);

        runner.SetPlayerObject(player, networkPlayerObject);

        playerList.Add(player, networkPlayerObject);
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
    #region unuse callbacks function


    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
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
    #endregion
}
