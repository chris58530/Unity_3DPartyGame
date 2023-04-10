using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBody : MonoBehaviour
{
    [SerializeField]
    private float detectionRadius;
    [SerializeField]
    private float magneticForce;
    [SerializeField]
    private AnimationCurve magnetForceCurve;

    MeshRenderer meshRenderer;
    Rigidbody rb;
    PlayerMoveInput moveInput;
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        moveInput = GetComponentInParent<PlayerMoveInput>();
        meshRenderer = GetComponent<MeshRenderer>();

    }
    private void Start()
    {

    }
    void Update()
    {
        MagentChange();
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


    public void SetRepel(Vector3 forceDirection, Rigidbody rb, float magneticForce) // Repulsion logic
    {
        rb.AddForce((transform.position - forceDirection).normalized * magneticForce);

    }
    public void SetAttract(Vector3 forceDirection, Rigidbody rb, float magneticForce) // Attraction logic
    {
        rb.AddForce((forceDirection - transform.position).normalized * magneticForce);
    }
    void MagentChange()
    {
        if (moveInput.positiveArrow&&!moveInput.nagativeArrow)
        {
            this.tag = "Positive";

            Vector3 currentScale = this.transform.localScale;
            if (currentScale.x < detectionRadius)
            {
                this.transform.localScale += new Vector3(10, 10, 10) * Time.deltaTime;
            }
            else
            {
                meshRenderer.material.color = new Color(1, 0, 0, 0.3f);
            }
            if (moveInput.nagativeArrow)
            {

                this.transform.localScale = new Vector3(0, 0, 0);
                return;
            }


        }
        else if (moveInput.nagativeArrow &&!moveInput.positiveArrow)
        {
            this.tag = "Negative";
            Vector3 currentScale = this.transform.localScale;
            if (currentScale.x < detectionRadius)
            {
                transform.localScale += new Vector3(10, 10, 10) * Time.deltaTime;
            }
            else
            {
                meshRenderer.material.color = new Color(0, 0, 1, 0.3f);
            }
          
        }
        else
        {
            this.transform.localScale = new Vector3(0, 0, 0);
            meshRenderer.material.color = new Color(0, 0, 0, 0f);
            this.tag = "None";
        }
    }
}