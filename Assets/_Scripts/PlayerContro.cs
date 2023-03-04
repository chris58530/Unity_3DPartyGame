using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerContro : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float rotationspeed;
    InputMaster inputmaster;//actionmap���ͣxC#
    float maxSpeed = 10;
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody>();

        inputmaster = new InputMaster();
        inputmaster.PlayerNormal.Enable();//����player�@�몬�A,�Y�O�i�J�����i�H�bactionmap�W�[�@��playerinwater��actionmap,�ǥѶ}�������ӨM�w���A
    }

    void Update()
    {
        Vector2 vector2d = inputmaster.PlayerNormal.Movement.ReadValue<Vector2>();//���player�o�iactionmap��movement�o��map��WASD�^�ǭ�(x,y)
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        if (vector2d != Vector2.zero)//�P�_�覡���W�U�@��(1,0)(-1,0)�M���k�@��(0,1)(0,-1)��2���V�q,�W�k�@�_���i�o(0.7,0.7)
        {
            Vector3 TargetVector = new Vector3(vector2d.x, 0, vector2d.y);//�N���o��2���ƭ���Ʀ��b���a���ʪ�3���ƭ�,�������J����0
            Moving(TargetVector);//����o���V�q�ǵ����ʤ�k
            RotateTowardMovementVector(TargetVector);//�¦V���o���V�q
        }
    }
    private void Moving(Vector3 dir)//����
    {
        if (dir.magnitude == 0) { return; }//�ٸ귽
        rb.velocity = dir * speed;//�V�q���W�t��
    }
    private void RotateTowardMovementVector(Vector3 dir)//�N�Y��V���ʤ�V
    {
        if (dir.magnitude == 0) { return; }//�ٸ귽
        var rotation = Quaternion.LookRotation(dir);// ��Quaternion.LookRotation ��J���o��3���V�q
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationspeed);//�N�Y�C�C��V�ؼд¦V
    }
}
