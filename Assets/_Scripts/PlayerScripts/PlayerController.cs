using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    PlayerGroundDetector playerGroundDetector;
    PlayerMoveInput playerMoveInput;
    GameObject model_1;
    GameObject model_2;

    public bool IsGround => playerGroundDetector.IsGround;
    public bool IsFalling => !IsGround && rb.velocity.y < 0f;
    public float walkSpeed;
    public float rushSpeed;
    public float switchToRush = 1;
    ///////////
    Vector3 originalTrans;
    ///////////
    private void Awake()
    {
        playerMoveInput = GetComponent<PlayerMoveInput>();
        rb = GetComponent<Rigidbody>();
        playerGroundDetector = GetComponentInChildren<PlayerGroundDetector>();
        model_1 = transform.GetChild(0).gameObject;
        model_2 = transform.GetChild(1).gameObject;
    }
    void Start(){
        playerMoveInput.EnableGamePlayInputs();
        originalTrans = this.transform.position;
    }
    public void SetPlayerAddForce( float speed)
    {
        float x =playerMoveInput.AxisX;
        float z =playerMoveInput.AxisZ;
        Vector3 output = Vector3.zero;
        output.x = x * Mathf.Sqrt(1 - (z * z) / 2.0f);
        output.z = z * Mathf.Sqrt(1 - (x * x) / 2.0f);

        if (playerMoveInput.Move)
        {
            Vector3 dir = new Vector3(x,0,z);
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
    public void SwitchTag(string tag){
        transform.tag = tag;
    }
    public void Throwing(){

    }
    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("DeadZone")){
            Debug.Log("back");
            transform.position = originalTrans;
        }   if(other.gameObject.CompareTag("Rush")){
            Debug.Log("rush");
            Vector3 output = (transform.position-other.transform.position).normalized;
        rb.AddForce(output * 100,ForceMode.Impulse);
            
        }
    }


}
