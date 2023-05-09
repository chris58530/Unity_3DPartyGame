using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody))]
// ReSharper disable once CheckNamespace
public class NetworkPlayerController : NetworkBehaviour, IMagnet
{
    Rigidbody rb;
    PlayerGroundDetector groundDetector;
    [Networked]
    public bool IsGround { get; set; }
    [Networked]
    public bool IsFalling { get; set; }
    public bool IsStun;
    public bool GetKnock;
    public float walkSpeed;
    public float rushSpeed;
    public float switchToRush = 1;
    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
    }
    void Update()
    {
        IsGround = groundDetector.IsGround;
        IsFalling = !IsGround && rb.velocity.y < 0f;
    }
    public void SetPlayerMove(NetworkInputData input)
    {
        if (NetworkPlayer.Local)
        {
            float x = input.AxisX;
            float z = input.AxisZ;
            Vector3 output = Vector3.zero;
            output.x = x * Mathf.Sqrt(1 - (z * z) / 2.0f);
            output.z = z * Mathf.Sqrt(1 - (x * x) / 2.0f);
            if (input.Move)
                SetPlayerLookAtForward(output);
            rb.AddForce(output * walkSpeed);
        }
    }
    public void SetPlayerRush(NetworkInputData input)
    {
        if (NetworkPlayer.Local)
        {
            float x = input.AxisX;
            float z = input.AxisZ;
            Vector3 output = Vector3.zero;
            output.x = x * Mathf.Sqrt(1 - (z * z) / 2.0f);
            output.z = z * Mathf.Sqrt(1 - (x * x) / 2.0f);

            rb.AddForce(output * rushSpeed);

            if (input.Move)
                SetPlayerLookAtForward(output);
        }
    }
    public void SetPlayerJump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
    public void SetPlayerFallDown(float speed)
    {
        rb.AddForce(Vector3.up * speed);
    }
    public void SwitchTag(string tag)
    {
        rb.transform.tag = tag;
    }
    #region SetPlayerLookAtForward       
    private void SetPlayerLookAtForward(Vector3 lookAt)
    {
        Vector3 dir = new Vector3(lookAt.x, 0, lookAt.z);
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 16);
    }
    #endregion
    #region SetPlayerMagentBody

    void IMagnet.SetRepel(Vector3 direction, float force)
    {
        rb.AddForce((transform.position - direction).normalized * force);

    }

    void IMagnet.SetAttract(Vector3 direction, float force)
    {
        rb.AddForce((direction - transform.position).normalized * force);
    }
    #endregion


}