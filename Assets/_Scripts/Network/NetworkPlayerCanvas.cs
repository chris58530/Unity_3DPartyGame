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
    private TMP_Text playerName;
    [Networked(OnChanged = nameof(OnPlayerNameChanged))]
    public string PlayerName { get; set; }
    [SerializeField]
    public Image AngryBar;

    NetworkPlayerData data;
    void Update()
    {
        transform.LookAt(Camera.main.transform);

    }

    public override void Spawned()
    {
        /*if (!Runner.IsServer) return;
        foreach (NetworkPlayerData data in GameManager.Instance.PlayerList.Values)
        {
            NetworkPlayer.Local.gameObject.GetComponent<NetworkPlayerCanvas>().PlayerName = data.PlayerName;
        }*/
    }
    private static void OnPlayerNameChanged(Changed<NetworkPlayerCanvas> changed)
    {
        changed.Behaviour.playerName.text = changed.Behaviour.PlayerName;
    }



}
