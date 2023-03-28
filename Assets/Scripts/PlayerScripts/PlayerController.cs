using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //this class is player action  
    [SerializeField]
    private float detectionRadius;
    [SerializeField]
    private float magneticForce;
    Rigidbody rb;
    MagnetBody magnetBody;
    SphereCollider magnetCollider;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        magnetBody = GetComponent<MagnetBody>();
        magnetCollider = GetComponentInChildren<SphereCollider>();
    }
    private void Start()
    {
        magnetCollider.radius = detectionRadius;
    }

    public void SetPlayerAddForece(Vector3 dir, float speed)
    {
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        rb.AddForce(dir * speed);
    }
    public void SetPlayerVelocity(Vector3 dir, float speed)
    {
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        rb.velocity = transform.forward * speed;
    }
    public void SetPlayerJump(float jumpSpeed)
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }
    public void SetPlayerMagneticForce()
    {

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Negative") || other.CompareTag("Positive"))
        {
            string otherTag = other.tag;
            string myTag = this.tag;
            Transform target = other.transform;

            if (otherTag == myTag)
            {
                magnetBody.Repel(target, this.rb, magneticForce);
            }
            else
            {
                magnetBody.Attract(target, this.rb, magneticForce);
            }
        }
    }
}
