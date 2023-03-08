using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour
{

    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private KeyCode forward, back, left, right;

    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       AddForce();
    }
    void FixeUpdate()
    {
        
    }
    void AddForce()
    {
        if (Input.GetKey(forward))
        {
            rb.AddForce(Vector3.forward * MovementSpeed);
        }
        if (Input.GetKey(back))
        {
            rb.AddForce(Vector3.back * MovementSpeed);

        }
        if (Input.GetKey(left))
        {
            rb.AddForce(Vector3.left * MovementSpeed);

        }
        if (Input.GetKey(right))
        {
            rb.AddForce(Vector3.right * MovementSpeed);
        }

    }
}
