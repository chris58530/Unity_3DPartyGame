using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //this class is player action  
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
   
    public void SetPlayerAddForece(Vector3 dir,float speed)
    {      
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,8);
        rb.AddForce(dir*speed);
    }
    public void SetPlayerVelocity(Vector3 dir, float speed)
    {
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        rb.velocity = transform.forward * speed;
    }
    public void SetPlayerJump(float jumpSpeed)
    {
        rb.AddForce(Vector3.up * jumpSpeed,ForceMode.Impulse);
    }
  
}
