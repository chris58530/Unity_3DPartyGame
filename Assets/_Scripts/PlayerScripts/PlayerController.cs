using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    PlayerGroundDetector groundDetector;
    PlayerMoveInput moveInput;
    GameObject model_1;
    GameObject model_2;

    public bool IsGround => groundDetector.IsGround;
    public bool IsFalling => !IsGround && rb.velocity.y < 0f;
    public bool IsStun;
    public float walkSpeed;
    public float rushSpeed;
    public float switchToRush = 1;
    ///////////
    Vector3 originalTrans;
    ///////////
    private void Awake()
    {
        moveInput = GetComponent<PlayerMoveInput>();
        rb = GetComponent<Rigidbody>();
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        model_1 = transform.GetChild(0).gameObject;
        model_2 = transform.GetChild(1).gameObject;
    }
    void Start()
    {
        moveInput.EnableGamePlayInputs();
        originalTrans = this.transform.position;
    }
    public void SetPlayerAddForce(float speed)
    {
        float x = moveInput.AxisX;
        float z = moveInput.AxisZ;
        Vector3 output = Vector3.zero;
        output.x = x * Mathf.Sqrt(1 - (z * z) / 2.0f);
        output.z = z * Mathf.Sqrt(1 - (x * x) / 2.0f);

        if (moveInput.Move)
        {
            Vector3 dir = new Vector3(x, 0, z);
            var rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 8);
        }
        rb.AddForce(output * speed);
    }
    public void SetPlayerFallDown(float speed)
    {
        rb.AddForce(Vector3.up * speed);
    }
    public void SetPlayerJump(float jumpSpeed)
    {
        // rb.velocity = new Vector3(rb.velocity.x,jumpSpeed);
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }
    public void SwitchModel(int num)
    {
        if (num == 1)
        {
            if (!model_1.activeSelf)
                model_1.SetActive(true);
            model_2.SetActive(false);
        }
        else
        {
            if (!model_2.activeSelf)
                model_2.SetActive(true);
            model_1.SetActive(false);
        }
    }
    public void SwitchTag(string tag)
    {
        transform.tag = tag;
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            transform.position = originalTrans;
        }
        if (other.gameObject.CompareTag("Rush") && !IsStun)
        {
            //如果此物件速度大於other物件速度
            PlayerMoveInput otherInput = other.gameObject.GetComponent<PlayerMoveInput>();
            if (moveInput.speedtime < otherInput.speedtime)
            {
                //重製雙方speed time;

                moveInput.speedtime = 0;
                otherInput.speedtime = 0;
                moveInput.ShowRushSpeed(false);
                otherInput.ShowRushSpeed(false);

                Vector3 output = (transform.position - other.transform.position).normalized;
                rb.AddForce(output * 50, ForceMode.Impulse);
                StartCoroutine(Strun(2));
            }
        }
        IStrikeable hitObject = other.gameObject.GetComponent<IStrikeable>();
        if (hitObject != null && moveInput.speedtime > switchToRush)
        {
            hitObject.Knock(transform.position, moveInput.speedtime);
            Debug.Log("hitobject knock");
        }

    }
    IEnumerator Strun(float time)
    {
        IsStun = true;
        yield return new WaitForSeconds(time);
        IsStun = false;

    }


}
