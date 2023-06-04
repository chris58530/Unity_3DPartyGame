using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkPlayerAbility : NetworkBehaviour
{
    [SerializeField, Header("磁力裝置預製物")]
    private NetworkMagnet magnetPrefab;
    [SerializeField, Header("磁力持續時間")]
    private float keepTime;
    [SerializeField, Header("磁力最大範圍")]
    private float detectionRadius;

    [SerializeField, Header("能力冷卻時間")]
    private float CD;
    [SerializeField]
    private float AddSpeeTimeValue = 5;
    [Networked]
    public NetworkBool IsOpenMagnet { get; set; }
    [Networked]
    public NetworkBool CanOpenMagnet { get; set; }
    [Networked]
    public NetworkBool CanShootMagnet { get; set; }
    [Networked(OnChanged = nameof(OnOpenMagnet))]
    private int OpenMagnetTrigger { get; set; }

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
        IsOpenMagnet = false;
        CanOpenMagnet = false;
        CanShootMagnet = false;
    }
    public override void Render()
    {
        material.color = MagnetColor;
    }
    public override void FixedUpdateNetwork()
    {
        if (keepTimer.Expired(Runner))
        {
            Close();
        }
        // if (CanOpenMagnet)
        //     OpenMagnet();
        if (CanOpenMagnet)
        {
            OpenMagnetTrigger += 1;
            Debug.Log($"OpenMagnetTrigger : {OpenMagnetTrigger}");
        }
    }

    public void ShootMagnet()
    {
        Runner.Spawn(magnetPrefab, transform.position, transform.rotation, Object.InputAuthority);
        CanShootMagnet = false;
    }
    void Close()
    {
        IsOpenMagnet = false;
        transform.localScale = Vector3.zero;
        keepTimer = TickTimer.None;
        MagnetColor = (new Color(0, 0, 0, 0));
        tag = "None";
        Debug.Log("reset magnet to none");
    }
    void AddSpeeTime(Changed<NetworkPlayerAbility> changed)
    {
        changed.Behaviour.controller.SpeedTime += AddSpeeTimeValue;
        Debug.Log("Power 1");
    }

    private static void OnOpenMagnet(Changed<NetworkPlayerAbility> changed)
    {
        if (!changed.Behaviour.CanOpenMagnet) return;

        Vector3 currentScale = changed.Behaviour.transform.localScale;
        Debug.Log("Power 2");
        if (currentScale.x < changed.Behaviour.detectionRadius)
        {
            changed.Behaviour.MagnetColor = new Color(0, 0, 0, 0.1f);
            changed.Behaviour.transform.localScale += new Vector3(10, 10, 10) * changed.Behaviour.Runner.DeltaTime;
            Debug.Log($"magnet: opening....");
        }
        else
        {
            changed.Behaviour.IsOpenMagnet = true;
            changed.Behaviour.CanOpenMagnet = false;
            changed.Behaviour.MagnetColor = new Color(0, 0, 1, 0.2f);
            changed.Behaviour.tag = "Repel";
            changed.Behaviour.keepTimer = TickTimer.CreateFromSeconds(changed.Behaviour.Runner, changed.Behaviour.keepTime);
            if (!changed.Behaviour.CanShootMagnet) return; //如果可以射出Magnet為真
            changed.Behaviour.ShootMagnet();
        }
    }



    private static void OnMagnetPowerChanged(Changed<NetworkPlayerAbility> changed)
    {
        float angryValue = changed.Behaviour.PowerTrigger;

        if (angryValue >= (float)PowerValue.Power3)
        {
            //避免OnChanged沒有被偵測到
            changed.Behaviour.PowerTrigger = 0;
            if (changed.Behaviour.IsOpenMagnet) return;
            Debug.Log("use power3 ");
            changed.Behaviour.controller.AngryValue -= (float)PowerValue.Power3;
            changed.Behaviour.CanOpenMagnet = true;

            return;
        }
        else if ((angryValue - (float)PowerValue.Power1) > 0)
        {
            //Power level 1
            //避免OnChanged沒有被偵測到
            changed.Behaviour.PowerTrigger = 0;
            changed.Behaviour.AddSpeeTime(changed);
            changed.Behaviour.controller.AngryValue -= (float)PowerValue.Power1;


            return;
        }
        // else if (changed.Behaviour.PowerTrigger > (float)PowerValue.Power2 && changed.Behaviour.PowerTrigger < (float)PowerValue.Power3)
        // {
        //     //Power level 2
        //     changed.Behaviour.CanOpenMagnet = true;
        //     changed.Behaviour.controller.AngryValue = 0;
        //     return;
        // }
        // else if (changed.Behaviour.PowerTrigger >= (float)PowerValue.Power3)
        // {
        //     //Power level 3
        //     changed.Behaviour.CanOpenMagnet = true;
        //     changed.Behaviour.CanShootMagnet = true;
        //     changed.Behaviour.controller.AngryValue = 0;
        //     return;
        // }
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
