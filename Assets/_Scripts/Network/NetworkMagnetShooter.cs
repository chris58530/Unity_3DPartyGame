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
    [Networked(OnChanged = nameof(OnMagnetOpenChanged))]
    public int OpenTrigger { get; set; }
    [Networked(OnChanged = nameof(OnMagnetCloseChanged))]
    public int CloseTrigger { get; set; }
    [Networked]
    public Color MagnetColor { get; set; }


    [Networked]
    public int MagnetCount { get; set; }
    private TickTimer CDTimer { get; set; }
    [Networked]
    private TickTimer keepTimer { get; set; }
    private Material material;
    public NetworkObject magnet;
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
        // if (CDTimer.ExpiredOrNotRunning(Runner))
        // {
        // }
        if (keepTimer.Expired(Runner))
        {
            IsOpenMagnet = false;

            keepTimer = TickTimer.None;
        }
        Debug.Log($"{keepTimer.Expired(Runner)}");

    }

    public void ShootMagnet(NetworkInputData input)
    {
        Runner.Spawn(magnetPrefab, transform.position, transform.rotation, Object.InputAuthority);
    }
    void Open(Changed<NetworkMagnetShooter> changed)
    {
        if (changed.Behaviour.IsOpenMagnet) return;
        Vector3 currentScale = changed.Behaviour.transform.localScale;
        if (currentScale.x < changed.Behaviour.detectionRadius)
        {
            changed.Behaviour.MagnetColor = new Color(0, 0, 0, 0.1f);
            changed.Behaviour.transform.localScale += new Vector3(20, 20, 20) * changed.Behaviour.Runner.DeltaTime;
            // Debug.Log($"magent:{currentScale} opening....");

        }
        else
        {
            changed.Behaviour.IsOpenMagnet = true;
            changed.Behaviour.MagnetColor = new Color(0, 0, 1, 0.2f);
            changed.Behaviour.tag = "Repel";
            changed.Behaviour.keepTimer = TickTimer.CreateFromSeconds(changed.Behaviour.Runner, changed.Behaviour.keepTime);
            Debug.Log($"magentLife:{changed.Behaviour.keepTime} ");
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

    private static void OnMagnetOpenChanged(Changed<NetworkMagnetShooter> changed)
    {
        changed.Behaviour.Open(changed);
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
