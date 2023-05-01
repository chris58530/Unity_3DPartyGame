using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour, INetworkRunnerCallbacks
{
    private PairState pairState = PairState.Lobby;

    [SerializeField] 
    private NetworkPlayerData playerNetworkDataPrefab = null;

    [SerializeField] 
    private RoomListPanel roomListPanel = null;
    [SerializeField] 
    private CreateRoomPanel createRoomPanel = null;
    [SerializeField] 
    private InRoomPanel inRoomPanel = null;

    private async void Start()
    {
        SetPairState(PairState.Lobby);


        GameManager.Instance.Runner.AddCallbacks(this);

        await JoinLobby(GameManager.Instance.Runner);
    }

    #region - Create Lobby & Room -
    public async Task JoinLobby(NetworkRunner runner)
    {
        var result = await runner.JoinSessionLobby(SessionLobby.ClientServer);

        if (!result.Ok)
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
    }

    public async Task CreateRoom(string roomName, int maxPlayer)
    {
        var result = await GameManager.Instance.Runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SessionName = roomName,
            PlayerCount = maxPlayer,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = GameManager.Instance.gameObject.AddComponent<NetworkSceneManagerDefault>(),
            // ObjectPool = gameManager.gameObject.AddComponent<FusionObjectPoolRoot>()
        });

        if (result.Ok)
            SetPairState(PairState.InRoom);
        else
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
    }

    public async Task JoinRoom(string roomName)
    {
        var result = await GameManager.Instance.Runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionName = roomName,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = GameManager.Instance.gameObject.AddComponent<NetworkSceneManagerDefault>(),
            // ObjectPool = gameManager.gameObject.AddComponent<FusionObjectPoolRoot>()
        });

        if (result.Ok)
            SetPairState(PairState.InRoom);
        else
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
    }
    #endregion

    #region - Set State -
    public void SetPairState(PairState newState)
    {
        pairState = newState;

        switch (pairState)
        {
            case PairState.Lobby:
                SetPanel(roomListPanel);
                break;
            case PairState.CreatingRoom:
                SetPanel(createRoomPanel);
                break;
            case PairState.InRoom:
                SetPanel(inRoomPanel);
                break;
        }
    }

    private void SetPanel(IPanel panel)
    {
        roomListPanel.DisplayPanel(false);
        createRoomPanel.DisplayPanel(false);
        inRoomPanel.DisplayPanel(false);

        panel.DisplayPanel(true);
    }
    #endregion

    #region - Used Callbacks -
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        roomListPanel.UpdateRoomList(sessionList);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        runner.Spawn(playerNetworkDataPrefab, Vector3.zero, Quaternion.identity, player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (GameManager.Instance.PlayerList.TryGetValue(player, out NetworkPlayerData networkPlayerData))
        {
            runner.Despawn(networkPlayerData.Object);

            GameManager.Instance.PlayerList.Remove(player);
            GameManager.Instance.UpdatePlayerList();
        }
    }

    #endregion

    #region - Unused callbacks -
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }


    #endregion
}
