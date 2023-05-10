using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody))]
// ReSharper disable once CheckNamespace
public class NetworkPlayerController : NetworkBehaviour, IMagnet
{
    Rigidbody rb;
    PlayerGroundDetector groundDetector;
    NetworkPlayerInput moveInput;
    [Networked]
    public bool IsGround { get; set; }
    [Networked]
    public bool IsFalling { get; set; }
    [Networked]
    public bool IsStun { get; set; }
    [Networked(OnChanged = nameof(OnModelChanged))]
    public int IsDefualtModel { get; set; }

    [SerializeField]
    private NetworkObject DefualtModel;
    [SerializeField]
    private NetworkObject RushModel;
    [Networked]
    private TickTimer StunTimer { get; set; }

    public bool GetKnock;
    public float walkSpeed;
    public float rushSpeed;
    public float switchToRush = 1;
    public float jumpForce;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();

    }
    void Update()
    {
        IsGround = groundDetector.IsGround;
        IsFalling = !IsGround && rb.velocity.y < 0f;
    }
    public override void FixedUpdateNetwork()
    {
        if (StunTimer.Expired(Runner))
        {
            IsStun = false;
        }
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
                SetPlayerLookAtForward(output);
            rb.AddForce(output * walkSpeed);
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

            rb.AddForce(output * rushSpeed);

            if (input.Move)
                SetPlayerLookAtForward(output);
        }
    }
    public void SetPlayerJump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
    public void SetPlayerFallDown(float speed)
    {
        rb.AddForce(Vector3.up * speed);
    }
    public void SwitchTag(string tag)
    {
        rb.transform.tag = tag;
    }
    public void SwitchModel(int num)
    {
        if (num == 1)
        {
            if (!DefualtModel.gameObject.activeSelf)
                DefualtModel.gameObject.SetActive(true);
            RushModel.gameObject.SetActive(false);
        }
        else
        {
            if (!RushModel.gameObject.activeSelf)
                RushModel.gameObject.SetActive(true);
            DefualtModel.gameObject.SetActive(false);
        }
    }
    private static void OnModelChanged(Changed<NetworkPlayerController> changed)
    {
        if (changed.Behaviour.DefualtModel.gameObject.activeSelf)
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
    void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject.CompareTag("DeadZone"))
        // {
        //     transform.position = originalTrans;
        // }
        if (other.gameObject.CompareTag("Rush") && !IsStun)
        {
            //如果此物件速度大於other物件速度
            NetworkPlayerInput otherInput = other.gameObject.GetComponent<NetworkPlayerInput>();
            if (moveInput.SpeedTime < otherInput.SpeedTime)
            {
                //重製雙方speed time;
                moveInput.SpeedTime = 0;
                otherInput.SpeedTime = 0;
                // moveInput.ShowRushSpeed(false);
                // otherInput.ShowRushSpeed(false);

                //擊飛
                Vector3 output = (transform.position - other.transform.position).normalized;
                rb.AddForce(output, ForceMode.Impulse);

                //暈兩秒開始計時
                StunTimer = TickTimer.CreateFromSeconds(Runner, 2);
                IsStun = true;
            }
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

    void IMagnet.SetRepel(Vector3 direction, float force)
    {
        rb.AddForce((transform.position - direction).normalized * force);

    }

    void IMagnet.SetAttract(Vector3 direction, float force)
    {
        rb.AddForce((direction - transform.position).normalized * force);
    }
    #endregion


}