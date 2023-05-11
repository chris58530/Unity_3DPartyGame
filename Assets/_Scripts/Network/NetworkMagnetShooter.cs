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
    private bool CanShoot { get; set; }
    [Networked]

    private bool CanOpen { get; set; }
    [Networked]
    private float CDTimer { get; set; }

    [Networked]
    private TickTimer timer { get; set; }
    private MeshRenderer mesh;

    public override void Spawned()
    {
        mesh = GetComponent<MeshRenderer>();

        //初始化CD
        CanOpen = true;
        CanShoot = false;
        CDTimer = 0;
    }
    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            if (CDTimer > 0)
            {
                CDTimer -= Runner.DeltaTime;
            }
            else
                CanOpen = true;
        }
        if (timer.ExpiredOrNotRunning(Runner)) { }


    }
    public void OpenMagnet()//蓄力
    {
        if (!CanOpen)return;
        if(!Object.HasStateAuthority)return;

        Vector3 currentScale = transform.localScale;
        if (currentScale.x < detectionRadius)
        {
            OpenMagneColor_RPC(new Color(0, 0, 0, 0.1f));
            transform.localScale += new Vector3(10, 10, 10) * Runner.DeltaTime;
            Debug.Log($"magent:{currentScale} opening....");
        }
        else
        {
            OpenMagneColor_RPC(new Color(0, 0, 1, 0.3f));
            this.tag = "Repel";
            CanShoot = true;
            CDTimer = CD;

        }
    }
    public void StopMagnet()//停止蓄力
    {

        transform.localScale = Vector3.zero;
        OpenMagneColor_RPC(new Color(0, 0, 0, 0));
        this.tag = "None";
    }
    public void ShootMagnet()
    {

        if (!CanShoot)
        {
            StopMagnet();
            return;
        }
        CanOpen = false;
        CanShoot = false;
        Runner.Spawn(magnetPrefab, transform.position, transform.rotation, Object.InputAuthority);
        StopMagnet();
        Debug.Log($"shoot magnet");

    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void OpenMagneColor_RPC(Color color)
    {
        mesh.material.color = color;
    }
}
