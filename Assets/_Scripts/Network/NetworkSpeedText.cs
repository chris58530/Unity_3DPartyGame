using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
public class NetworkSpeedText : NetworkBehaviour
{
    [SerializeField]
    public TMP_Text speedText;
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

}
