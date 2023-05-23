using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RoadMapLoop : NetworkBehaviour
{
    
    private Material material;

    [SerializeField]
    private float roadSpeed;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }
    
    public  void Update()
    {
      material.mainTextureOffset += new Vector2(1, 0) * Time.deltaTime * roadSpeed;
    }
}
