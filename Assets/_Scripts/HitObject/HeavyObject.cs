using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyObject : HitObject
{
    private void Start()
    {
        rb.mass = 5;
    }
    public void Knock(Vector3 forcePoint, float force)
    {
        rb.AddForce(-forcePoint * knockForce * force, ForceMode.Impulse);
        this.tag = "KnockingObject";
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rush"))
        {
            if (!other.gameObject.TryGetComponent<PlayerController>(out PlayerController otherControl))
                return;
            Vector3 direction = (otherControl.transform.position - transform.position).normalized * 100;
            otherControl.StartCoroutine(otherControl.Strun(direction, 1));
        }
    }
}
