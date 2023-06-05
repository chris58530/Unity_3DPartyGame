using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class Sign : NetworkBehaviour
{
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Rush")
        {
            AudioManager.Instance.RPC_PlaySFX("HitIron");
            return;
        }
    }

}
