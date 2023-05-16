using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;
public class NetworkPlayerCanvas : NetworkBehaviour
{
    [SerializeField]
    public TMP_Text speedText;
    [SerializeField]
    public TMP_Text playerName;
    [Networked(OnChanged = nameof(OnPlayerNameChanged))]
    public string PlayerName { get; set; }
    [SerializeField]
    public Image AngryBar;
    void Update()
    {
        transform.LookAt(Camera.main.transform);

    }

    public override void Spawned()
    {
        if (!Object.HasStateAuthority) return;
        foreach (NetworkPlayerData data in GameManager.Instance.PlayerList.Values)
        {
            PlayerName = data.PlayerName;

        }
    }
    private static void OnPlayerNameChanged(Changed<NetworkPlayerCanvas> changed)
    {
        if(!changed.Behaviour.Object.HasInputAuthority)return;
        changed.Behaviour.playerName.text = changed.Behaviour.PlayerName;
    }



}
