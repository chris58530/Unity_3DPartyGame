using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;


public class NetworkMagnet : NetworkBehaviour
{
    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private float flyTime;
    [Networked]
    private TickTimer lifeTimer { get; set; }
    [Networked]
    private TickTimer flyTimer { get; set; }
    [SerializeField]
    private AnimationCurve scaleCurve;


    [SerializeField]
    private float speed;

    [SerializeField]
    private float magnetForce = 70;
    private float startTime => Time.time - timer;
    float timer;
    Material material;

    void Start()
    {
        timer = Time.time;
    }

    public override void Spawned()
    {
        lifeTimer = TickTimer.CreateFromSeconds(Runner, lifeTime);
        flyTimer = TickTimer.CreateFromSeconds(Runner, flyTime);

        material = GetComponent<MeshRenderer>().material;
    }

    public override void FixedUpdateNetwork()
    {
        if (flyTimer.Expired(Runner))
        {
            float scale = scaleCurve.Evaluate(startTime - flyTime);

            transform.localScale = new Vector3(scale, scale, scale);
            this.tag = "Positive";
            material.color = new Color(1, 0, 0, 0.2f);

        }
        else transform.position += transform.forward * speed * Runner.DeltaTime;
        if (lifeTimer.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IMagnet magnet))
        {
            //用distance判斷距離，越靠近力量越大，之後magnetForce*算出來的數值
            if (other.gameObject.GetComponentInChildren<NetworkMagnetShooter>().IsOpenMagnet)
                return;
            magnet.SetAttract(transform.position, magnetForce);
        }
    }
}
