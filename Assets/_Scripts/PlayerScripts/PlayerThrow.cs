using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    PlayerMoveInput moveInput;
    LineRenderer line;
    [SerializeField]
    private GameObject magnetObject;
    [SerializeField]
    private float lineSpeed;

    float lineLength;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        moveInput = GetComponentInParent<PlayerMoveInput>();
    }
    void Update()
    {
        Throw();
    }
    void Throw()
    {
        if (moveInput.Throw)
        {
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
          
            Debug.Log(transform.TransformPoint(ee));
            TrowMagnet(transform.TransformPoint(ee));
        }
    }
    void TrowMagnet(Vector3 target)
    {
        GameObject magnet = Instantiate(magnetObject, transform.position, transform.rotation);
        magnet.GetComponent<MagnetDevice>().target = target;
    }
}
