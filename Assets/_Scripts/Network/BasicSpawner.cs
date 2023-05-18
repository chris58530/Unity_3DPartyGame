using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    private GameManager gameManager;
    private NetworkRunner networkRunner = null;
    [SerializeField]
    private NetworkPrefabRef[] playerPrefab = null;
    [SerializeField]
    private CinemachineTargetGroup camGroup;

    NetworkPlayerInput playerInput;

    public Dictionary<PlayerRef, NetworkObject> playerList = new Dictionary<PlayerRef, NetworkObject>();
    void Start()
    {
        gameManager = GameManager.Instance;
        networkRunner = gameManager.Runner;
        networkRunner.AddCallbacks(this);
        SpawnAllPlayers();

    }
    private void SpawnAllPlayers()
    {
        foreach (PlayerRef player in gameManager.PlayerList.Keys)
        {
            Vector3 spawnPosition = Vector3.zero;
            if (gameManager.PlayerList.TryGetValue(player, out NetworkPlayerData data))
            {
                NetworkObject networkPlayerObject = networkRunner.Spawn(playerPrefab[data.CharaterCount], spawnPosition, Quaternion.identity, player);


                networkRunner.SetPlayerObject(player, networkPlayerObject);

                playerList.Add(player, networkPlayerObject);

            }
        }

        NetworkPlayer[] players = FindObjectsOfType<NetworkPlayer>();
        foreach (NetworkPlayer player in players)
        {
            camGroup.AddMember(player.gameObject.transform, 1, 1);

        }


    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        // var data = new NetworkInputData();
        // data.AxisX = Input.GetAxisRaw("Horizontal");
        // data.AxisZ = Input.GetAxisRaw("Vertical");
        // Debug.Log(data.AxisX + data.AxisZ);

        if (NetworkPlayer.Local != null && playerInput == null)
        {
            playerInput = NetworkPlayer.Local.gameObject.GetComponent<NetworkPlayerInput>();
        }
        if (playerInput != null)
        {
            input.Set(playerInput.GetNetworkInput());
        }

        // if (playerList.TryGetValue(runner.LocalPlayer, out NetworkObject networkObject))
        // {
        //     NetworkPlayerInput data = networkObject.gameObject.GetComponent<NetworkPlayerInput>();
        //     input.Set(data.GetNetworkInput());
        // }
        // else Debug.Log("cant find playerinput");
    }


    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {

        Vector3 spawnPosition = Vector3.zero;
        NetworkObject networkPlayerObject = runner.Spawn(playerPrefab[gameManager.PlayerCharacter], spawnPosition, Quaternion.identity, player);

        runner.SetPlayerObject(player, networkPlayerObject);

        playerList.Add(player, networkPlayerObject);

    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (playerList.TryGetValue(player, out NetworkObject networkObject))
        {
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
