using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class TrainSpawner : NetworkBehaviour
{
    [SerializeField]
    private float spawnRate;
    [SerializeField]
    private NetworkPrefabRef trainPrefab;
    [SerializeField]
    private Transform[] spawnPoint;
    [Networked]
    private TickTimer spawnTimer { get; set; }
    [Networked]
    private int spawnTrainNum { get; set; }

    public override void Spawned()
    {
        spawnTimer = TickTimer.CreateFromSeconds(Runner, spawnRate);
        SpawnTrain();
    }
    public override void FixedUpdateNetwork()
    {
        if (spawnTimer.Expired(Runner))
        {
            SpawnTrain();
            spawnTimer = TickTimer.CreateFromSeconds(Runner, spawnRate);
        }

    }
    private void SpawnTrain()
    {
        spawnTrainNum = Random.Range(0, spawnPoint.Length);

        Runner.Spawn(trainPrefab, spawnPoint[spawnTrainNum].transform.position,
               spawnPoint[spawnTrainNum].transform.rotation, Object.InputAuthority);
    }
    private static void OnSpawnTrain(Changed<TrainSpawner> changed)
    {
        int spawnNum = changed.Behaviour.spawnTrainNum;
        changed.Behaviour.Runner.Spawn(changed.Behaviour.trainPrefab, changed.Behaviour.spawnPoint[spawnNum].transform.position,
         changed.Behaviour.spawnPoint[spawnNum].transform.rotation, changed.Behaviour.Object.InputAuthority);
    }
}
