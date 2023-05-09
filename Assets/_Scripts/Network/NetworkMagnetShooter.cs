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
    private bool CanShoot;
    private bool CanOpen;
    private MeshRenderer mesh;
    [Networked]
    private TickTimer CDTimer { get; set; }
    [Networked]
    private TickTimer keepTimer { get; set; }

    public override void Spawned()
    {
        //初始化CD
        CanOpen = true;
    }
    public override void FixedUpdateNetwork()
    {
        if (CDTimer.Expired(Runner))
        {
            CanOpen = true;
        }
        if (keepTimer.Expired(Runner) && CanShoot)
        {
            ShootMagnet();
        }

    }
    public void OpenMagnet()//蓄力
    {
        if (!CanOpen)
            return;
        Vector3 currentScale = this.transform.localScale;
        if (currentScale.x < detectionRadius)
            this.transform.localScale += new Vector3(10, 10, 10) * Runner.DeltaTime;
        else
        {
            OpenMagneColor_RPC(new Color(0, 0, 1, 0.3f));
            this.tag = "Repel";
            keepTimer = TickTimer.CreateFromSeconds(Runner, keepTime);
            return;
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
            return;
        CanOpen = false;
        Runner.Spawn(magnetPrefab, transform.position, transform.rotation, Object.InputAuthority);
        CDTimer = TickTimer.CreateFromSeconds(Runner, CD);
        CanShoot = false;

    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void OpenMagneColor_RPC(Color color)
    {
        mesh.material.color = color;
    }
}
