using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkMagnetShooter : NetworkBehaviour
{
    [SerializeField, Header("磁力裝置預製物")]
    private NetworkMagnet magnetPrefab;
    [SerializeField, Header("磁力持續時間")]
    private float keepTime;
    [SerializeField, Header("磁力最大範圍")]
    private float detectionRadius;

    [SerializeField, Header("能力冷卻時間")]
    private float CD;
    [Networked]
    public NetworkBool IsOpenMagnet { get; set; }

    [Networked(OnChanged = nameof(OnMagnetPowerChanged))]
    public float PowerTrigger { get; set; }


    [Networked]
    public Color MagnetColor { get; set; }
    [Networked]

    private TickTimer CDTimer { get; set; }
    [Networked]
    private TickTimer keepTimer { get; set; }
    private Material material;
    public GameObject magnet;
    NetworkPlayerController controller;
    void Awake()
    {
        controller = GetComponentInParent<NetworkPlayerController>();
    }

    public override void Spawned()
    {
        material = magnet.GetComponent<MeshRenderer>().material;
    }
    public override void Render()
    {
        material.color = MagnetColor;
    }
    public override void FixedUpdateNetwork()
    {
        if (keepTimer.Expired(Runner))
        {
            keepTimer = TickTimer.None;
            Close();
        }
        Power2();
        Debug.Log(controller.AngryValue);
    }

    public void ShootMagnet()
    {
        Runner.Spawn(magnetPrefab, transform.position, transform.rotation, Object.InputAuthority);
    }
    void Power2()
    {
        if (!IsOpenMagnet) return;
        Vector3 currentScale = transform.localScale;
        Debug.Log("power 2");
        if (currentScale.x < detectionRadius)
        {
            MagnetColor = new Color(0, 0, 0, 0.1f);
            transform.localScale += new Vector3(10, 10, 10) * Runner.DeltaTime;
            Debug.Log($"magnet: opening....");
        }
        else
        {
            IsOpenMagnet = false;
            MagnetColor = new Color(0, 0, 1, 0.2f);
            tag = "Repel";
            keepTimer = TickTimer.CreateFromSeconds(Runner, keepTime);
        }
    }
    void Close()
    {
        if (IsOpenMagnet) return;
        IsOpenMagnet = false;
        transform.localScale = Vector3.zero;
        MagnetColor = (new Color(0, 0, 0, 0));
        tag = "None";
    }
    void Power1(Changed<NetworkMagnetShooter> changed)
    {
        changed.Behaviour.controller.SpeedTime += 5;
        Debug.Log("power 1");


    }
    void Power3(Changed<NetworkMagnetShooter> changed)
    {
        IsOpenMagnet = true;
        if (!IsOpenMagnet) return;
        Vector3 currentScale = transform.localScale;
        Debug.Log("power 2");
        if (currentScale.x < detectionRadius)
        {
            MagnetColor = new Color(0, 0, 0, 0.1f);
            transform.localScale += new Vector3(10, 10, 10) * Runner.DeltaTime;
            Debug.Log($"magnet: opening....");
        }
        else
        {
            IsOpenMagnet = false;
            MagnetColor = new Color(0, 0, 1, 0.2f);
            tag = "Repel";
            keepTimer = TickTimer.CreateFromSeconds(Runner, keepTime);
            changed.Behaviour.ShootMagnet();
            Debug.Log("power 3");
        }


    }



    private static void OnMagnetPowerChanged(Changed<NetworkMagnetShooter> changed)
    {
        if (changed.Behaviour.PowerTrigger >= (float)PowerValue.Power1 && changed.Behaviour.PowerTrigger <= (float)PowerValue.Power2)
        {
            //Power level 1
            changed.Behaviour.Power1(changed);
            changed.Behaviour.controller.AngryValue = 0;

        }
        else if (changed.Behaviour.PowerTrigger > (float)PowerValue.Power2 && changed.Behaviour.PowerTrigger < (float)PowerValue.Power3)
        {
            //Power level 2
            changed.Behaviour.IsOpenMagnet = true;
            changed.Behaviour.controller.AngryValue = 0;
            Debug.Log("power 2");



        }
        else if (changed.Behaviour.PowerTrigger >= (float)PowerValue.Power3)
        {
            //Power level 3
            changed.Behaviour.Power3(changed);
            changed.Behaviour.controller.AngryValue = 0;

        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Repel"))
        {
            controller.SpeedTime = 0;
            Vector3 output = (transform.parent.position - other.transform.parent.position).normalized;
            controller.SetRepel(output, 20);
        }
    }

}
