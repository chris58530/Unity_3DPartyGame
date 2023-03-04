using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    private int core =0;
    [SerializeField]
    private TextMeshProUGUI textMeshPro;
    private void LateUpdate()
    {
            textMeshPro.text = core.ToString();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Back")
        {
            Debug.Log("++");

            core += 1;
        }
    }
}
