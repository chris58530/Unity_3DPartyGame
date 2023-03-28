using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBody : MonoBehaviour
{
    public void GetAttract(Rigidbody rb)
    {

    }

    public void Repel(Transform other, Rigidbody rb,float magneticForce) // Repulsion logic
    {      
        rb.AddForce(other.transform.position -transform.position * magneticForce);

    }
    public void Attract(Transform other , Rigidbody rb,float magneticForce) // Attraction logic
    {     
        rb.AddForce(transform.position - other.transform.position * magneticForce);
    }

}