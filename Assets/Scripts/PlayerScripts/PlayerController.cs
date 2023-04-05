using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    PlayerGroundDetector playerGroundDetector;

    public bool IsGround => playerGroundDetector.IsGround;
    public bool IsFalling => !playerGroundDetector.IsGround && rb.velocity.y < 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerGroundDetector = GetComponentInChildren<PlayerGroundDetector>();
    }
    private void Start()
    {
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
    public void SetPlayerAddForceY(float speed)
    {
        rb.AddForce(Vector3.up * speed);

    }
    public void SetPlayerJump(float jumpSpeed)
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }


}
