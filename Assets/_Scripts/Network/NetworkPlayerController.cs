using System;
using Fusion;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(NetworkRigidbody))]
// ReSharper disable once CheckNamespace
public class NetworkPlayerController : NetworkBehaviour, IMagnet
{
    NetworkRigidbody rb;
    PlayerGroundDetector groundDetector;

    [Networked]
    public bool IsGround { get; set; }
    [Networked]
    public bool IsFalling { get; set; }
    [Networked]
    public bool IsStun { get; set; }
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
    private NetworkPlayerCanvas playerCanvas;

    public float walkSpeed;
    public float rushSpeed;
    public float switchToRush = 1;
    public float jumpForce;
    private void Awake()
    {
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        playerCanvas = GetComponentInChildren<NetworkPlayerCanvas>();
    }
    public override void Spawned()
    {
        rb = GetComponent<NetworkRigidbody>();

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
        IsGround = groundDetector.IsGround;
        IsFalling = !IsGround && rb.Rigidbody.velocity.y < 0f;
    }
    public override void FixedUpdateNetwork()
    {
        if (StunTimer.ExpiredOrNotRunning(Runner))
        {
            IsStun = false;
        }
        if (GetInput(out NetworkInputData inputData))
        {
            if (inputData.Move && !IsStun)
                SpeedTime += Runner.DeltaTime;
            else
                SpeedTime = 0;
        }
        if (AngryValue > 0)
            AngryValue -= Runner.DeltaTime * 2;
        if (AngryValue >= 100)
            AngryValue = 100;

    }
    public void SetPlayerMove(NetworkInputData input)
    {
        if (NetworkPlayer.Local)
        {
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

    }
    public void SetPlayerRush(NetworkInputData input)
    {
        if (NetworkPlayer.Local)
        {
            float x = input.AxisX;
            float z = input.AxisZ;
            Vector3 output = Vector3.zero;
            output.x = x * Mathf.Sqrt(1 - (z * z) / 2.0f);
            output.z = z * Mathf.Sqrt(1 - (x * x) / 2.0f);

            rb.Rigidbody.AddForce(output * rushSpeed);

            if (input.Move)
                SetPlayerLookAtForward(output);
        }
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
            changed.Behaviour.DefualtModel.gameObject.SetActive(false);
            changed.Behaviour.RushModel.gameObject.SetActive(true);
        }
        else
        {
            changed.Behaviour.DefualtModel.gameObject.SetActive(true);
            changed.Behaviour.RushModel.gameObject.SetActive(false);
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
        if (other.gameObject.CompareTag("DeadZone"))
        {
            if (GameManager.Instance.PlayerList.TryGetValue(Object.InputAuthority, out var data))
            {
                if (!data.IsDead)
                {
                    data.IsDead = true;
                    BattleManager.Instance.currentPlayerCount -= 1;
                    Debug.Log($"(PlayerController)目前人數 : {BattleManager.Instance.currentPlayerCount}");
                    Debug.Log($"死亡玩家 : {data.PlayerName}");
                }
                else Runner.Despawn(Object);
            }
        }
        if (other.gameObject.CompareTag("Rush") && !IsStun)
        {
            //如果此物件速度大於other物件速度
            NetworkPlayerController otherInput = other.gameObject.GetComponent<NetworkPlayerController>();
            if (SpeedTime < otherInput.SpeedTime)
            {
                //重製雙方speed time;
                SpeedTime = 0;
                otherInput.SpeedTime = 0;
                // moveInput.ShowRushSpeed(false);
                // otherInput.ShowRushSpeed(false);

                //擊飛自己
                Vector3 output = (transform.position - other.transform.position).normalized;
                SetRepel(output, 50);

                //暈兩秒開始計時
                StunTimer = TickTimer.CreateFromSeconds(Runner, 2);
                IsStun = true;
            }
        }
        if (other.gameObject.CompareTag("Repel"))
        {
            SpeedTime = 0;
        }
        //撞擊普通物件場景物件

        // if (other.gameObject.tag == "KnockingObject")
        // {
        //     Vector3 direction = -other.transform.position.normalized;
        //     StartCoroutine(Strun(direction * 5, 2));
        //     Debug.Log("KnockingObject knock");
        // }
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
    #endregion


}