using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private int layer;
    
    private void OnEnable()
    {
        camera.cullingMask |= (1 << layer); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            int _layer = other.gameObject.layer;
            camera.cullingMask |= (1 << _layer);
            Debug.Log("enter" + _layer);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int _layer = other.gameObject.layer;
            camera.cullingMask &= ~(1 << _layer);
            Debug.Log("exit" + _layer);

        }
    }
}
