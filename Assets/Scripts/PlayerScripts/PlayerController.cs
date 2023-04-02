using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //this class is player action  
    public static Vector3 MoveVelocity;
    public Vector3 MoveSpeed => new Vector3((rb.velocity.x), 0, (rb.velocity.z));
    public float MoveSpeedX => Mathf.Abs(rb.velocity.x);
    public float MoveSpeedZ => Mathf.Abs(rb.velocity.z);
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void Start()
    {
    }

    public void SetPlayerAddForece(Vector3 dir, float speed)
    {
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        rb.AddForce(dir * speed);
    }
    public void SetPlayerVelocity(Vector3 dir, float speed)
    {
        Vector3 lookAt = new Vector3(dir.x,rb.velocity.y,dir.z);
        var rotation = Quaternion.LookRotation(lookAt);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        // rb.velocity = lookAt * speed;
        print(((lookAt*speed)+MagnetBody.MagnetForce));
        rb.velocity = ((lookAt*speed)+MagnetBody.MagnetForce);
    }
    public void SetPlayerVelocity(Vector3 speed)
    {
        float x = speed.x;
        float z = speed.z;
        rb.velocity = new Vector3(x, 0, z);
    }
    public void SetPlayerJump(float jumpSpeed)
    {
        //rb.AddForce(Vector3.up * jumpSpeed);
    }


}
