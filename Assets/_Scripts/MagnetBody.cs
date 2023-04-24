using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBody : MonoBehaviour
{
    public bool IsMagnetTag => this.tag == "Repel";
    [SerializeField]
    private float detectionRadius;
    [SerializeField]
    private float magneticForce;
    [SerializeField]
    private AnimationCurve magnetForceCurve;
    [SerializeField, Header("磁力持續時間")]
    private float magnetTime;
    float currentMagnetTime;
    bool isDoneMagnet;
    MeshRenderer meshRenderer;
    Rigidbody rb;
    PlayerMoveInput moveInput;
    PlayerController controller;
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        moveInput = GetComponentInParent<PlayerMoveInput>();
        controller = GetComponentInParent<PlayerController>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        ResetMagnet();
        isDoneMagnet = false;
    }
    void Update()
    {
        MagentChange();
        if (isDoneMagnet)
        {
            currentMagnetTime -= Time.deltaTime;
        }
        if (currentMagnetTime < 0)
        {
            ResetMagnet();
        }
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
                SetRepel(target, magneticForce + additionByCurve);
            }
            else
            {
                SetAttract(target, magneticForce + additionByCurve);
            }
        }
    }
    #region IMagnet function
    public void SetRepel(Vector3 forceDirection, float magneticForce) // Repulsion logic
    {
        rb.AddForce((transform.position - forceDirection).normalized * magneticForce);
    }
    public void SetAttract(Vector3 forceDirection, float magneticForce) // Attraction logic
    {
        rb.AddForce((forceDirection - transform.position).normalized * magneticForce);
    }
    #endregion
    void MagentChange()
    {
        if (moveInput.MagnetZone)
        {
            ScaleMagentZone("Repel", new Color(0, 0, 1, 0.3f));
        }     
        if (!moveInput.MagnetZone || controller.IsStun)
        {
            ResetMagnet();
        }
    }
    void ResetMagnet()
    {
        currentMagnetTime = magnetTime;
        isDoneMagnet = false;
        this.transform.localScale = new Vector3(0, 0, 0);
        meshRenderer.material.color = new Color(0, 0, 0, 0f);
        this.tag = "None";
    }
    void ScaleMagentZone(string tag, Color color)
    {
        Vector3 currentScale = this.transform.localScale;
        if (currentScale.x < detectionRadius)
            this.transform.localScale += new Vector3(10, 10, 10) * Time.deltaTime;
        else
        {
            meshRenderer.material.color = color;
            this.tag = tag;
            isDoneMagnet = true;
            return;
        }
    }
}
