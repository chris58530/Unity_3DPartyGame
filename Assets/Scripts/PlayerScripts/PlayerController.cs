using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    PlayerGroundDetector playerGroundDetector;
    GameObject model_1;
    GameObject model_2;

    public bool IsGround => playerGroundDetector.IsGround;
    public bool IsFalling => !playerGroundDetector.IsGround && rb.velocity.y < 0;
    public float walkSpeed;
    public float rushSpeed;
    public float switchToRush = 1;
    // public float rushValue{get;private set;}
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerGroundDetector = GetComponentInChildren<PlayerGroundDetector>();
        model_1 = transform.GetChild(0).gameObject;
        model_2 = transform.GetChild(1).gameObject;
    }

    public void SetPlayerAddForce(Vector3 dir, float speed)
    {

        Vector3 output = Vector3.zero;
        output.x = dir.x * Mathf.Sqrt(1 - (dir.z * dir.z) / 2.0f);
        output.z = dir.z * Mathf.Sqrt(1 - (dir.x * dir.x) / 2.0f);

        if (dir != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        }
        rb.AddForce(output * speed);
    }
    public void SetPlayerFallDown(float speed)
    {
        rb.AddForce(Vector3.up * speed);

    }
    public void SetPlayerJump(float jumpSpeed)
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }
    public void SwitchModel(int num)
    {
        if (num == 1)
        {
            if (!model_1.activeSelf)
                model_1.SetActive(true);
            model_2.SetActive(false);
        }
        else
        {
            if (!model_2.activeSelf)
                model_2.SetActive(true);
            model_1.SetActive(false);
        }
    }

}
