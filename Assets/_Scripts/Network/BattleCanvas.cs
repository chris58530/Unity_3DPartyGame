using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;

public class BattleCanvas : NetworkBehaviour
{
    [SerializeField]
    public TMP_Text timeText;
    [SerializeField]
    public Image[] PlayerValue;

    [Networked]
    public int Score { get; set; }

    private void OnEnable()
    {
       
    }
    private static void OnPlayerScoreChanged(Changed<BattleCanvas> changed)
    {

    }
}
