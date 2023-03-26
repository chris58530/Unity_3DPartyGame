using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInput : MonoBehaviour
{
    [SerializeField]
    public KeyCode forwardArrow, backArrow, leftArrow, rightArrow;


    public Vector2 moveInput { get; private set; }

    float v,h=0 ;


    void Update()
    {
        
        if (Input.GetKey(forwardArrow))        
            v = 1;        
        else if (Input.GetKey(backArrow))        
            v = -1;
        
        if (Input.GetKey(rightArrow))        
            h = 1;        
        else if (Input.GetKey(leftArrow))        
            h = -1;       
       
     
        moveInput = new Vector2(v, h);
    }
}
