using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    

    [Header("True = Positive, False = Negative")]
    [SerializeField] public bool polarity = true;
    [SerializeField] public float magneticForce;
    [SerializeField] public GameObject positiveMag;
    [SerializeField] public GameObject negativeMag;
    private Rigidbody rb;
    private GameObject otherGameobj;
    private SphereCollider field;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        field = this.GetComponent<SphereCollider>();
        showMagnetic();
    }
    void showMagnetic()
    {
        if (polarity)
        {
            positiveMag.SetActive(true);
            negativeMag.SetActive(false);
        }
        else
        {
            positiveMag.SetActive(false);
            negativeMag.SetActive(true);
        }
    
    }
    void OnTriggerStay(Collider other) 
    {
        otherGameobj = other.gameObject;
        Rigidbody otherrb = GetComponent<Rigidbody>();
        if (other.tag == "Positive")//??????~????
        {
            if (this.polarity == false)//??????
            {
                Attract(otherGameobj, otherrb);
            }
            else                    //?????t
            { 
                Repel(otherGameobj, otherrb);
            }
        }
        else if (other.tag == "Negative")//??????~???t
        {
            if (this.polarity == true)//??????
            {
                Attract(otherGameobj, otherrb);
            }
            else                    //?????t
            {
                Repel(otherGameobj, otherrb);
            }
        }
    }
    void SwitchMag()
    {
        if (polarity)
        {
            polarity = false;
            this.tag = "Negative";
        }
        else
        { 
            polarity = true;
            this.tag = "Positive";
        }   

        showMagnetic();
    }
    void Attract(GameObject other , Rigidbody otherrb) // Attraction logic
    {
        print(this.name+"??C");
        //if (otherrb.mass > rb.mass) // Moves smaller magnet towards player
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 0.2f);

        //}
        //else if (otherrb.mass <= rb.mass) // Moves player towards larger magnet
        //{
        //    other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, 0.2f);

        //}
        //otherrb.AddForce(transform.position - other.transform.position * magneticForce);
        rb.AddForce(other.transform.position -transform.position * magneticForce);
        otherrb.AddForce(other.transform.position - transform.position * magneticForce);
    }
    void Repel(GameObject other, Rigidbody otherrb) // Repulsion logic
    {
        print(this.name + "???");
        //if (other.transform.localScale.x > transform.localScale.x) // Moves smaller magnet away from player
        //{
        //    Vector3 vector = transform.position - other.transform.position;
        //    float distance = Mathf.Clamp(Vector3.Magnitude(vector), 5f, 10f);
        //    vector.Normalize();
        //    vector *= 1 / distance;
        //    transform.position += vector;
        //}
        //else if (other.transform.localScale.x <= transform.localScale.x) // Moves player away from larger magnet
        //{
        //    Vector3 vector = other.transform.position - transform.position;
        //    float distance = Mathf.Clamp(Vector3.Magnitude(vector), 5f, 10f);
        //    vector.Normalize();
        //    vector *= 1 / distance;
        //    other.transform.position += vector;
        //}
        //rb.AddForce(transform.position - other.transform.position * magneticForce);
        otherrb.AddForce(transform.position - other.transform.position * magneticForce);

    }
}
