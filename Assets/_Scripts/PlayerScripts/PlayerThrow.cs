using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    PlayerMoveInput moveInput;
    LineRenderer line;
    [SerializeField]
    private GameObject magnetObjectRed;
    [SerializeField]
    private GameObject magnetObjectBlue;
    [SerializeField]
    private float lineSpeed;

    float lineLength;
    bool canTrow = false;
    string magnetTag;
    MagnetBody magnetBody;

    void Awake()
    {
        line = GetComponentInChildren<LineRenderer>();
        moveInput = GetComponent<PlayerMoveInput>();
        magnetBody = GetComponentInChildren<MagnetBody>();
    }
    void Update()
    {
        if (magnetBody.IsMagnetTag || canTrow)
            TrowMagnet();
    }
    void TrowMagnet()
    {
        if (moveInput.Throw)
        {
            magnetTag = magnetBody.LastTag;
            canTrow = true;
            line.enabled = true;
            if (lineLength <= 10)//line 最大長度
            {
                lineLength += lineSpeed * Time.deltaTime;
            }
            line.SetPosition(1, new Vector3(0, 0, lineLength));
        }
        else if (moveInput.StopThrow)
        {
            line.enabled = false;
            lineLength = 0;
            Vector3 ee = line.GetPosition(1);
            if (magnetTag== "Positive")
            {
                GameObject magnet = Instantiate(magnetObjectRed, transform.position, transform.rotation);
                magnet.GetComponent<MagnetDevice>().target = transform.TransformPoint(ee);
                canTrow = false;
            }
            else
            {
                GameObject magnet = Instantiate(magnetObjectBlue, transform.position, transform.rotation);
                magnet.GetComponent<MagnetDevice>().target = transform.TransformPoint(ee);
                canTrow = false;

            }
        }
    }

}


