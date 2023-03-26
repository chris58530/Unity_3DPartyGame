using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    public void SetPlayerVelocity(float speed)
    {
        rb.AddForce(Vector3.forward * speed);
    }
    public void SetPlayerHorizontal(float speed)
    {
        rb.AddForce(Vector3.right * speed);
    }
}
