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
   
    public void SetPlayerVelocity(Vector3 dir,float speed)
    {      
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,8);
        rb.AddForce(dir*speed);
    }
    public void SetPlayerJump()
    {

    }
  
}
