using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDevice : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private AnimationCurve scaleCurve;
    [SerializeField]
    private float timeToGrow;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float magnetForce = 150;
    [HideInInspector]
    public Vector3 target;
    private float startTime => Time.time - timer;
    float timer;

    void Start()
    {
        timer = Time.time;
    }
    void Update()
    {
        if (startTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);

        if (startTime > timeToGrow)
        {
            float scale = scaleCurve.Evaluate(startTime - timeToGrow);
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IMagnet magnet))
        {
            if (other.tag == this.tag)
            {
                magnet.SetRepel(transform.position, magnetForce);
                Debug.Log("setrepel");
            }
            else
                magnet.SetAttract(transform.position, magnetForce);
        }
    }

}
