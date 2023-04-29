using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody))]
// ReSharper disable once CheckNamespace
public class NetworkPlayerController : NetworkBehaviour
{
    Rigidbody rb;
    PlayerGroundDetector groundDetector;
    NetworkPlayerInput moveInput;
    public bool IsGround => groundDetector.IsGround;
    public bool IsFalling => !IsGround && rb.velocity.y < 0f;
    public bool IsStun;
    public bool GetKnock;
    public float walkSpeed;
    public float rushSpeed;
    public float switchToRush = 1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveInput = GetComponent<NetworkPlayerInput>();
    }
    public void SetPlayerAddForce(float speed)
    {
        float x = moveInput.AxisX;
        float z = moveInput.AxisZ;
        Vector3 output = Vector3.zero;
        output.x = x * Mathf.Sqrt(1 - (z * z) / 2.0f);
        output.z = z * Mathf.Sqrt(1 - (x * x) / 2.0f);

        if (moveInput.Move)
        {
            Vector3 dir = new Vector3(x, 0, z);
            var rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        }
        rb.AddForce(output * speed);
    }
}