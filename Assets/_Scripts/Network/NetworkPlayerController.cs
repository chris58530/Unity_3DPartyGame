using System;
using Fusion;
using UnityEngine;
using TMPro;
using Cinemachine;


[RequireComponent(typeof(NetworkRigidbody))]
// ReSharper disable once CheckNamespace
public class NetworkPlayerController : NetworkBehaviour, IMagnet
{
    NetworkRigidbody rb;
    #region Ground detect
    [SerializeField, Range(0.1f, 1f)]
    private float detectionRadius = 0.1f;
    [SerializeField]
    private LayerMask layer;
    Collider[] colliders = new Collider[1];

    public bool IsGround;
    public bool IsFalling;
    #endregion
    [Networked]
    public bool IsStun { get; set; }
    [Networked]
    public bool IsBall { get; set; }
    [Networked(OnChanged = nameof(OnModelChanged))]
    public int modelCount { get; set; }
    [Networked(OnChanged = nameof(OnSpeedTimeChanged))]
    public float SpeedTime { get; set; }
    [Networked(OnChanged = nameof(OnAngryValueChanged))]
    public float AngryValue { get; set; }

    [SerializeField]
    private GameObject DefualtModel;
    [SerializeField]
    private GameObject RushModel;
    [Networked]
    private TickTimer StunTimer { get; set; }
    private float repelTime;


    private NetworkPlayerCanvas playerCanvas;


    private CinemachineTargetGroup camGroup;

    [SerializeField]
    private int dragValue = 10;

    public float walkSpeed;
    public float rushSpeed;
    public float switchToRush = 1;
    public float jumpForce;
    private Vector3 defualtScale;
    private Vector3 rushScale;
    private void Awake()
    {
        //  groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        playerCanvas = GetComponentInChildren<NetworkPlayerCanvas>();
    }
    public override void Spawned()
    {
        rb = GetComponent<NetworkRigidbody>();

        defualtScale = DefualtModel.transform.localScale;
        rushScale = RushModel.transform.localScale;
        RushModel.gameObject.transform.localScale = Vector3.zero;
        if (Object.HasInputAuthority)
        {
           camGroup = FindObjectOfType<CinemachineTargetGroup>();
        }


        // if (Object.HasInputAuthority)
        // {
        //     rb.InterpolationDataSource = InterpolationDataSources.Predicted;
        // }
        // else
        // {
        //     rb.InterpolationDataSource = InterpolationDataSources.Snapshots;
        // }

    }
    private void Update()
    {
        GroundDetect();
        IsFalling = !IsGround && rb.Rigidbody.velocity.y < 1f;
    }
    public override void FixedUpdateNetwork()
    {
        DetectCollision();

        if (StunTimer.ExpiredOrNotRunning(Runner))
        {
            IsStun = false;
        }
        // if (GetInput(out NetworkInputData inputData))
        // {
        //     if (inputData.Move && !IsStun)
        //         SpeedTime += Runner.DeltaTime;
        //     else
        //         SpeedTime = 0;
        // }
        if (modelCount == 1)
        {
            SpeedTime += Runner.DeltaTime;
        }
        else SpeedTime = 0;

        if (AngryValue > 0)
            AngryValue -= Runner.DeltaTime * 2;
        if (AngryValue >= 100)
            AngryValue = 100;

        float newDragValue = dragValue - (SpeedTime * 1f);
        rb.Rigidbody.drag = newDragValue < 0f ? 0f : newDragValue;

    }
    public void SetPlayerMove(NetworkInputData input)
    {
        if (!NetworkPlayer.Local) return;
        
            float x = input.AxisX;
            float z = input.AxisZ;
            Vector3 output = Vector3.zero;
            output.x = x * Mathf.Sqrt(1 - (z * z) / 2.0f);
            output.z = z * Mathf.Sqrt(1 - (x * x) / 2.0f);
            if (input.Move)
            {
                SetPlayerLookAtForward(output);
            }
            rb.Rigidbody.AddForce(output * walkSpeed);

        

    }
    public void SetPlayerRush(NetworkInputData input)
    {
        if (!NetworkPlayer.Local) return;

        float x = input.AxisX;
        float z = input.AxisZ;
        Vector3 output = Vector3.zero;
        output.x = x * Mathf.Sqrt(1 - (z * z) / 2.0f);
        output.z = z * Mathf.Sqrt(1 - (x * x) / 2.0f);

        if (input.Move)
        {
            SetPlayerLookAtForward(output);
            rb.Rigidbody.AddForce(output * (rushSpeed + (SpeedTime * 0.01f)));
        }
        else
            rb.Rigidbody.AddForce(transform.forward * (rushSpeed + (SpeedTime * 0.01f)));

    }
    public void SetPlayerJump()
    {
        rb.Rigidbody.AddForce(Vector3.up * jumpForce);
    }
    public void SetPlayerFallDown(float speed)
    {
        rb.Rigidbody.AddForce(Vector3.up * speed);
    }
    public void SwitchTag(string tag)
    {
        rb.transform.tag = tag;
    }
    private static void OnModelChanged(Changed<NetworkPlayerController> changed)
    {

        if (changed.Behaviour.modelCount == 1)
        {
            changed.Behaviour.DefualtModel.gameObject.transform.localScale = Vector3.zero;
            changed.Behaviour.RushModel.gameObject.transform.localScale = changed.Behaviour.rushScale;
            // changed.Behaviour.DefualtModel.gameObject.SetActive(false);
            // changed.Behaviour.RushModel.gameObject.SetActive(true);
        }
        else
        {
            changed.Behaviour.DefualtModel.gameObject.transform.localScale = changed.Behaviour.defualtScale;
            changed.Behaviour.RushModel.gameObject.transform.localScale = Vector3.zero;
            // changed.Behaviour.DefualtModel.gameObject.SetActive(true);
            // changed.Behaviour.RushModel.gameObject.SetActive(false);
        }
    }
    private static void OnSpeedTimeChanged(Changed<NetworkPlayerController> changed)
    {
        changed.Behaviour.playerCanvas.speedText.text = Mathf.Round(changed.Behaviour.SpeedTime).ToString();
    }
    private static void OnAngryValueChanged(Changed<NetworkPlayerController> changed)
    {
        changed.Behaviour.playerCanvas.AngryBar.fillAmount = changed.Behaviour.AngryValue / 100;

    }

    void OnCollisionStay(Collision other)
    {
        if (!NetworkPlayer.Local) return;

        var data = GameManager.Instance.PlayerList.TryGetValue(Object.InputAuthority, out var playerData) ? playerData : null;
        if (data.IsDead) return;
        
        if (other.gameObject.CompareTag("DeadZone"))
        {
            if (!GameManager.Instance.Runner.IsServer) return;
            data.IsDead = true;
            BattleManager.Instance.currentPlayerCount -= 1;
            Debug.Log($"(PlayerController)目前人數 : {BattleManager.Instance.currentPlayerCount}");
            Debug.Log($"死亡玩家 : {data.PlayerName}");

           
            return;
        }
        if (other.gameObject.CompareTag("Rush") && !IsStun)
        {
            //如果此物件速度大於other物件速度
            NetworkPlayerController otherInput = other.gameObject.GetComponent<NetworkPlayerController>();
            if (SpeedTime < otherInput.SpeedTime)
            {
                //重製己方speed time;
                SpeedTime = 0;
                // otherInput.SpeedTime = 0;
                // moveInput.ShowRushSpeed(false);
                // otherInput.ShowRushSpeed(false);

                //擊飛自己
                Vector3 output = (transform.position - other.transform.position).normalized;
                float addForce = otherInput.SpeedTime;
                SetRepel(output, 150 + addForce);

                //暈兩秒開始計時
                StunTimer = TickTimer.CreateFromSeconds(Runner, 2.5f);
                IsStun = true;
            }
        }
        if (other.gameObject.CompareTag("Repel"))
        {
            SpeedTime = 0;
            //擊飛自己
            Vector3 output = (transform.position - other.transform.position).normalized;
            SetRepel(output, 150);

            //暈兩秒開始計時
            StunTimer = TickTimer.CreateFromSeconds(Runner, 2.5f);
            IsStun = true;
        }
        //撞擊普通物件場景物件

        if (other.gameObject.tag == "Untouched")
        {
            if (other.gameObject.TryGetComponent<HeavyObject>(out HeavyObject obj))
            {
                Vector3 output = (transform.position - other.transform.position).normalized;
                SetRepel(output, obj.KnockForce);//擊飛數值在對方身上
                //暈兩秒開始計時
                StunTimer = TickTimer.CreateFromSeconds(Runner, 2.5f);
                IsStun = true;
            }
        }
        // IStrikeable hitObject = other.gameObject.GetComponent<IStrikeable>();
        // if (hitObject != null && moveInput.SpeedTime > switchToRush && other.gameObject.tag == "HitObject")
        // {
        //     Vector3 direction = (transform.position - other.gameObject.transform.position).normalized;
        //     hitObject.Knock(direction, moveInput.SpeedTime);
        //     Debug.Log("hitobject knock");
        // }
    }


    #region SetPlayerLookAtForward       
    private void SetPlayerLookAtForward(Vector3 lookAt)
    {
        Vector3 dir = new Vector3(lookAt.x, 0, lookAt.z);
        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 16);
    }
    #endregion
    #region SetPlayerMagentBody

    public void SetRepel(Vector3 direction, float force)
    {
        rb.Rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
    
    public void SetAttract(Vector3 direction, float force)
    {
        rb.Rigidbody.AddForce((direction - transform.position).normalized * force);
    }
    private void GroundDetect()
    {
        if (Physics.OverlapSphereNonAlloc(transform.position - new Vector3(0, -0.5f, 0), detectionRadius, colliders, layer) != 0)
        {
            IsGround = true;
        }
        else IsGround = false;
    }
    #endregion
    private void DetectCollision()
    {
        if (Object == null) return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.8f);
        bool isColliderFound = false;

        foreach (var collider in colliders)
        {

            if (collider.GetComponent<Truck>())
            {
                isColliderFound = true;
                transform.parent = collider.transform;
                Debug.Log(collider.name);
                break;
            }
            // if (collider.gameObject.CompareTag("DeadZone"))
            // {
            //     if (GameManager.Instance.PlayerList.TryGetValue(Object.InputAuthority, out var data))
            //     {
            //         data.IsDead = true;
            //         BattleManager.Instance.currentPlayerCount -= 1;
            //         Debug.Log($"(PlayerController)目前人數 : {BattleManager.Instance.currentPlayerCount}");
            //         Debug.Log($"死亡玩家 : {data.PlayerName}");
            //         Runner.Despawn(Object);
            //         break;
            //     }
            // }

        }
        if (!isColliderFound)
        {
            transform.parent = null;
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, -0.5f, 0), detectionRadius);
    }

}