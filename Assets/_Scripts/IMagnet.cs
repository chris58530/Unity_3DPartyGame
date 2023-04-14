using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagnet
{
    void SetRepel(Vector3 direction ,float force);
    void SetAttract(Vector3 direction, float force);

}

