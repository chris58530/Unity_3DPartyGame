using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInput : MonoBehaviour
{
    [SerializeField]
    public KeyCode forwardArrow, backArrow, leftArrow, rightArrow;
    public Vector2 moveInput { get; private set; }
    public float speedtime { get; private set; }

    float v, h = 0;

    void Update()
    {

        v = Input.GetKey(forwardArrow) ? 1 : (Input.GetKey(backArrow) ? -1 : 0);
        h = Input.GetKey(rightArrow) ? 1 : (Input.GetKey(leftArrow) ? -1 : 0);
        speedtime += (Input.GetKey(forwardArrow) || Input.GetKey(backArrow) || Input.GetKey(leftArrow) || Input.GetKey(rightArrow)) ? Time.deltaTime : ((speedtime >= 0) ? -Time.deltaTime*10 : 0);
        moveInput = new Vector2(v, h);
    }
}
