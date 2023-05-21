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
    [Networked(OnChanged = nameof(OnMagnetCloseChanged))]
    public int ClosePowerTrigger { get; set; }

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
        }

    }

    public void ShootMagnet()
    {
        Runner.Spawn(magnetPrefab, transform.position, transform.rotation, Object.InputAuthority);
    }
    void Power2()
    {
        // if (changed.Behaviour.IsOpenMagnet) return;
        // Vector3 currentScale = changed.Behaviour.transform.localScale;
        // float targetScale = changed.Behaviour.detectionRadius;
        // Debug.Log("power 2");

        // if (currentScale.x < targetScale)
        // {
        //     changed.Behaviour.MagnetColor = new Color(0, 0, 0, 0.1f);
        //     changed.Behaviour.transform.localScale += new Vector3(10, 10, 10) * changed.Behaviour.Runner.DeltaTime;
        //     Debug.Log($"magnet: opening....");
        // }
        // else
        // {
        //     changed.Behaviour.IsOpenMagnet = true;
        //     changed.Behaviour.MagnetColor = new Color(0, 0, 1, 0.2f);
        //     changed.Behaviour.tag = "Repel";
        //     changed.Behaviour.keepTimer = TickTimer.CreateFromSeconds(changed.Behaviour.Runner, changed.Behaviour.keepTime);
        // }
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
    void Close(Changed<NetworkMagnetShooter> changed)
    {
        if (changed.Behaviour.IsOpenMagnet) return;
        changed.Behaviour.IsOpenMagnet = false;
        changed.Behaviour.transform.localScale = Vector3.zero;
        changed.Behaviour.MagnetColor = (new Color(0, 0, 0, 0));
        changed.Behaviour.tag = "None";
    }
    void Power1(Changed<NetworkMagnetShooter> changed)
    {
        changed.Behaviour.controller.SpeedTime += 5;
        Debug.Log("power 1");


    }
    void Power3(Changed<NetworkMagnetShooter> changed)
    {
        // changed.Behaviour.Power2(changed);
        changed.Behaviour.ShootMagnet();
        Debug.Log("power 3");
    }



    private static void OnMagnetPowerChanged(Changed<NetworkMagnetShooter> changed)
    {
        if (changed.Behaviour.PowerTrigger >= 33 && changed.Behaviour.PowerTrigger <= 66)
        {
            //Power level 1
            changed.Behaviour.Power1(changed);
            changed.Behaviour.controller.AngryValue = 0;

        }
        else if (changed.Behaviour.PowerTrigger > 66 && changed.Behaviour.PowerTrigger < 99)
        {
            //Power level 2
            changed.Behaviour.IsOpenMagnet = true;
            changed.Behaviour.controller.AngryValue = 0;


        }
        else if (changed.Behaviour.PowerTrigger >= 99)
        {
            //Power level 3
            changed.Behaviour.Power3(changed);
            changed.Behaviour.controller.AngryValue = 0;

        }
    }
    private static void OnMagnetCloseChanged(Changed<NetworkMagnetShooter> changed)
    {
        changed.Behaviour.Close(changed);
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
