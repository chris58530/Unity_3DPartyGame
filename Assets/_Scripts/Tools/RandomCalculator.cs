using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCalculator : Singleton<RandomCalculator>
{
    protected override void Awake()
    {
        base.Awake();
    }
    public int GetRandom(int valueLength)
    {
        int num = Random.Range(0,valueLength);
        return num;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
