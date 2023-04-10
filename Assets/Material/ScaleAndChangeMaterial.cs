using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAndChangeMaterial : MonoBehaviour
{
 public float size = 2.0f; // 要放大的倍數
    public Material material; // 要更換的材質

    private bool isScaling = false; // 是否正在放大
    private Vector3 initialScale; // 初始大小
    private float scaleSpeed = 1.0f; // 放大速度
    private float timer = 0.0f; // 計時器

    // 當按下按鍵時，設定計時器為2.5秒
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            timer = 2.5f;
        }
    }

    void FixedUpdate()
    {
        // 如果計時器仍在計時，則開始放大物體
        if (timer > 0.0f)
        {
            // 初始大小
            if (!isScaling)
            {
                initialScale = transform.localScale;
                isScaling = true;
            }

            // 放大速度，可以根據需要自行調整
            scaleSpeed += 0.1f;

            // 放大物體
            transform.localScale = Vector3.Lerp(initialScale, initialScale * size, scaleSpeed * Time.deltaTime);

            // 計時器倒數
            timer -= Time.deltaTime;

            // 放大完畢後，更換材質
            if (timer <= 0.0f)
            {
                GetComponent<Renderer>().material = material;
            }
        }
    }
}
