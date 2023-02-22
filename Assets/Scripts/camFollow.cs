using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public float panSpeed = 20f;
    public float panEdge = 10f;
    public float scrollSpeed = 20f;
    public Vector2 hightLimt;
    public Vector2 ZLimt;
    public Vector2 camLimt;
    private bool camLock = true;

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = transform.position;
        //捲動縮放
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Pos.y += scroll * scrollSpeed * 100f * Time.deltaTime;
        Pos.y = Mathf.Clamp(Pos.y, hightLimt.x, hightLimt.y);
        //切換鏡頭鎖定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            camLock = !camLock;
        }

        //鎖定視角
        if (camLock)
        {
            //Z軸偏移y=ax+b a=0.5
            offset.z = Pos.y * -0.5f;
            offset.z = Mathf.Clamp(offset.z, ZLimt.x, ZLimt.y);
            //相機跟隨
            Vector3 desiredPos = player.position + offset;
            Pos.x = desiredPos.x;
            Pos.z = desiredPos.z;
            transform.position = Pos;

        }

        //自由視角
        if(camLock == false)
        {
            if (Input.mousePosition.y >= Screen.height - panEdge)
            {
            Pos.z += panSpeed * Time.deltaTime;
            }
             if (Input.mousePosition.y <= panEdge)
            {
            Pos.z -= panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x >= Screen.width - panEdge)
             {
            Pos.x += panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x <= panEdge)
            {
            Pos.x -= panSpeed * Time.deltaTime;
            }
            //移動限制
            Pos.x = Mathf.Clamp(Pos.x, -camLimt.x, camLimt.x);
            Pos.z = Mathf.Clamp(Pos.z, -camLimt.y, camLimt.y);
            transform.position = Pos;
        }
        
    }
}
