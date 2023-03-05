using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class P2 : MonoBehaviour
{
    InputMaster inputmaster;//actionmap產生ㄉC#
    private Rigidbody rb;
    public float speed;
    public float rotationspeed;
    public float maxSpeed = 10;
    public float jumpStrength = 10;
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody>();


        inputmaster = new InputMaster();
        inputmaster.player2.Enable();//此為player一般狀態,若是進入水中可以在actionmap增加一個playerinwater的actionmap,藉由開啟關閉來決定狀態
        //inputActions.FindAction("Player");
        inputmaster.player2.Jump.performed += Jump;
    }

    void FixedUpdate()
    {
        Vector2 vector2d = inputmaster.player2.Movement.ReadValue<Vector2>();//獲取player這張actionmap中movement這個map的WASD回傳值(x,y)
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        if (vector2d != Vector2.zero)//判斷方式為上下一組(1,0)(-1,0)和左右一組(0,1)(0,-1)的2維向量,上右一起按可得(0.7,0.7)
        {
            Vector3 TargetVector = new Vector3(vector2d.x, 0, vector2d.y);//將取得的2維數值轉化成在平地移動的3維數值,中間插入高度0
            Moving(TargetVector);//把取得的向量傳給移動方法
            RotateTowardMovementVector(TargetVector);//朝向取得的向量
        }
    }
    private void Moving(Vector3 dir)//移動
    {
        if (dir.magnitude == 0) { return; }//省資源
        rb.AddForce(dir * speed, ForceMode.Acceleration);//向量乘上速度
    }
    private void RotateTowardMovementVector(Vector3 dir)//將頭轉向移動方向
    {
        if (dir.magnitude == 0) { return; }//省資源
        var rotation = Quaternion.LookRotation(dir);// 用Quaternion.LookRotation 將取得的3為向量轉為四元數
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationspeed);//將頭慢慢轉向目標朝向
    }
    private void Jump(InputAction.CallbackContext ctx)//移動
    {
        rb.AddForce(Vector3.up * jumpStrength);
    }
}

