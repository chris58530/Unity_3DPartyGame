using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBody : MonoBehaviour
{
    public static Vector3 MagnetForce;
    [SerializeField]
    private float detectionRadius;
    [SerializeField]
    private float magneticForce;
    [SerializeField]
    private AnimationCurve magnetForceCurve;
    MeshRenderer meshRenderer;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        transform.localScale = new Vector3(detectionRadius, detectionRadius, detectionRadius);
    }
    void Update()
    {
        ChangeMagnetic();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<MagnetBody>(out MagnetBody otherBody))
        {
            string otherTag = other.tag;
            string myTag = this.tag;
            Vector3 target = otherBody.gameObject.transform.position;
            float distance = Vector3.Distance(transform.position, other.transform.position);
            float additionByCurve = magnetForceCurve.Evaluate(distance);

            if (otherTag == myTag)
            {
                SetRepel(target, this.rb, magneticForce + additionByCurve);
            }
            else
            {
                SetAttract(target, this.rb, magneticForce + additionByCurve);

            }
        }
        else { return; };
    }
    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<MagnetBody>(out MagnetBody otherBody))
        {
            MagnetForce = new Vector3(0, 0, 0);
        }
    }

    public void SetRepel(Vector3 forceDirection, Rigidbody rb, float magneticForce) // Repulsion logic
    {
        rb.AddForce((transform.position - forceDirection) * magneticForce);

        //MagnetForce = ((transform.position - forceDirection) * magneticForce);
    }
    public void SetAttract(Vector3 forceDirection, Rigidbody rb, float magneticForce) // Attraction logic
    {
        rb.AddForce((forceDirection - transform.position) * magneticForce);
        // MagnetForce= (-(transform.position - forceDirection) * magneticForce);
    }
    private void ChangeMagnetic()
    {
        if (this.tag == "Positive")
        {
            meshRenderer.material.color = new Color(1, 0,0, 0.3f);

        }
        else if (this.tag == "Negative")
        {
            meshRenderer.material.color = new Color(0, 0, 1, 0.3f);
        }else{
            meshRenderer.material.color = new Color(0, 0, 0, 0f);

        }
    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, detectionRadius);
    // }

}