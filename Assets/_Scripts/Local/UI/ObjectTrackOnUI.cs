using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectTrackOnUI : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TMP_Text playerPositionText;
    [SerializeField]
    private Vector3 offset;

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(player.transform.position);
       
        playerPositionText.rectTransform.anchoredPosition = screenPos + offset;
    }
}