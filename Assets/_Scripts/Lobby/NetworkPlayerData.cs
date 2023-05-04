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

    [Networked] 
    public Color TankColor { get; set; }

    [Networked] 
    public Color BarrelColor { get; set; }

    public override void Spawned()
    {     

        transform.SetParent(GameManager.Instance.transform);

        GameManager.Instance.PlayerList.Add(Object.InputAuthority, this);
        GameManager.Instance.UpdatePlayerList();

        if (Object.HasInputAuthority)
        {
            SetPlayerName_RPC(GameManager.Instance.PlayerName);
            SetTankColor_RPC(GameManager.Instance.TankColor);
            SetBarrelColor_RPC(GameManager.Instance.BarrelColor); 
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
    public void SetTankColor_RPC(Color color)
    {
        TankColor = color;
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void SetBarrelColor_RPC(Color color)
    {
        BarrelColor = color;
    }
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
    #endregion

}