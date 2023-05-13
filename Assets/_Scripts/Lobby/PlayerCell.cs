using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCell : MonoBehaviour
{
    [SerializeField]
    private TMP_Text playerNameTxt = null;
    [SerializeField]
    private Image isReadyImg = null;
    [SerializeField]
    private Image notReadyImg = null;

    private string playerName = null;
    private int CharaterCount = 0;
    private bool isReady = false;

    public void SetInfo(string playerName, bool isReady, int CharaterCount)
    {
        this.playerName = playerName;
        this.isReady = isReady;
        this.CharaterCount = CharaterCount;
        playerNameTxt.text = this.playerName;
        if (this.isReady)
        {
            isReadyImg.enabled = true;
            notReadyImg.enabled = false;
        }
        else
        {
            isReadyImg.enabled = false;
            notReadyImg.enabled = true;
        }
    }
}
