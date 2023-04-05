using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInput : MonoBehaviour
{
    [SerializeField]
    public KeyCode forwardArrow, backArrow, leftArrow, rightArrow, jumpArrow, magentArrow;
    public Vector2 moveInput { get; private set; }
    public float speedtime { get; private set; }
    public bool Jump => Input.GetKeyDown(jumpArrow);
    public bool StopJump => !Input.GetKeyDown(jumpArrow);

    float v, h = 0;
    bool magent = true;
    void Update()
    {

        v = Input.GetKey(forwardArrow) ? 1 : (Input.GetKey(backArrow) ? -1 : 0);
        h = Input.GetKey(rightArrow) ? 1 : (Input.GetKey(leftArrow) ? -1 : 0);
        if (Input.GetKey(forwardArrow) || Input.GetKey(backArrow) || Input.GetKey(leftArrow) || Input.GetKey(rightArrow))
        {
            speedtime += Time.deltaTime;
        }
        else
        {
            if (speedtime >= 0)
            {
                speedtime -= Time.deltaTime * 50;
            }
            else
            {
                speedtime = 0;
            }
        }
        moveInput = new Vector2(v, h);

        if (Input.GetKeyDown(magentArrow))
        {
            GameObject magentDetector = transform.GetComponentInChildren<MagnetBody>().gameObject;
            if (magentDetector.tag == "Negative")
                magentDetector.tag = "Positive";
            else
                magentDetector.tag = "Negative";

        }
    }
}
