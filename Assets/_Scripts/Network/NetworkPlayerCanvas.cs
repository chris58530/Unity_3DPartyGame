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
    public Image AngryBar;
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

}
