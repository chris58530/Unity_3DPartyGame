using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCell : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text playerNameTxt = null;
    [SerializeField] 
    private TMP_Text isReadyTxt = null;

    private string playerName = null;
    private bool isReady = false;

    public void SetInfo(string playerName, bool isReady)
    {
        this.playerName = playerName;
        this.isReady = isReady;

        playerNameTxt.text = this.playerName;
        isReadyTxt.text = this.isReady ? "Ready" : "";
    }
}
