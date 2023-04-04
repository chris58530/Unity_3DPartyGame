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
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        rb.AddForce(dir * speed);
    }
    public void SetPlayerVelocity(Vector3 dir, float speed)
    {
        Vector3 lookAt = new Vector3(dir.x, rb.velocity.y, dir.z);
        var rotation = Quaternion.LookRotation(lookAt);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        // rb.velocity = lookAt * speed;
        print(((lookAt * speed) + MagnetBody.MagnetForce));
        rb.velocity = ((lookAt * speed) + MagnetBody.MagnetForce);
    }
    public void SetPlayerVelocityY(float speed)
    {
        // rb.velocity.y =
    }
    public void SetPlayerJump(float jumpSpeed)
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }


}
