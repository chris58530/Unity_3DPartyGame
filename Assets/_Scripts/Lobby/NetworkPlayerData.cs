using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkPlayerData : NetworkBehaviour
{

    [Networked(OnChanged = nameof(OnPlayerNameChanged))] 
    public string PlayerName { get; set; }
    [Networked(OnChanged = nameof(OnIsReadyChanged))] 
    public NetworkBool IsReady { get; set; }

    [Networked(OnChanged = nameof(OnCharaterNameChanged))] 
    public int CharaterCount { get; set; }

    public override void Spawned()
    {     

        transform.SetParent(GameManager.Instance.transform);

        GameManager.Instance.PlayerList.Add(Object.InputAuthority, this);
        GameManager.Instance.UpdatePlayerList();

        if (Object.HasInputAuthority)
        {
            SetPlayerName_RPC(GameManager.Instance.PlayerName);
            SetCharacterCount_RPC(GameManager.Instance.PlayerCharacter);
        }
    }

    #region - RPCs -

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void SetPlayerName_RPC(string name)
    {
        PlayerName = name;
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void SetReady_RPC(bool isReady)
    {
        IsReady = isReady;
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void SetCharacterCount_RPC(int obj)
    {
        CharaterCount = obj;
    }

    // [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    // public void SetBarrelColor_RPC(Color color)
    // {
    //     BarrelColor = color;
    // }
    #endregion

    #region - OnChanged Events -
    private static void OnPlayerNameChanged(Changed<NetworkPlayerData> changed)
    {
        GameManager.Instance.UpdatePlayerList();
    }

    private static void OnIsReadyChanged(Changed<NetworkPlayerData> changed)
    {
        GameManager.Instance.UpdatePlayerList();
    }
    private static void OnCharaterNameChanged(Changed<NetworkPlayerData> changed){
        GameManager.Instance.UpdatePlayerList();
    }
    #endregion

}
