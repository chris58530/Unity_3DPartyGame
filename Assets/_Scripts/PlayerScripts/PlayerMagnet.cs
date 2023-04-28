using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    PlayerMoveInput moveInput;
    [SerializeField]
    private GameObject magnetObjectAttract;
    MagnetBody magnetBody;
    void Awake()
    {
        moveInput = GetComponent<PlayerMoveInput>();
        magnetBody = GetComponentInChildren<MagnetBody>();
    }
    void Update()
    {
        ExpandAndTrowMagnet();
    }

    void ExpandAndTrowMagnet()
    {
        if (moveInput.StopThrow && magnetBody.IsMagnetTag)
        {
            Throw();

        }
        else
        {
            Debug.Log(moveInput.StopThrow);
            Debug.Log(magnetBody.IsMagnetTag);
        }

    }
    void Throw()
    {
        Debug.Log("throw");
        magnetBody.isShoot = false;
        Instantiate(magnetObjectAttract, transform.position, transform.rotation);

    }

}


