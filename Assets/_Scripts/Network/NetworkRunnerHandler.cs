using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;
    NetworkRunner networkRunner;
    // Start is called before the first frame update
    void Start()
    {
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "Network Runner";

        var clientTask = InitializeNetworkRunner(networkRunner,GameMode.AutoHostOrClient,
        NetAddress.Any(),SceneManager.GetActiveScene().buildIndex,null);

        Debug.Log("server start");
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner,GameMode mode ,NetAddress address,
    SceneRef scene,Action<NetworkRunner> initailize){
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if(sceneManager == null){
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }
        runner.ProvideInput = true;
        return runner.StartGame(new StartGameArgs{
            GameMode = mode,
            Address = address,
            Scene = scene,
            SessionName = "Network Fusion",
            Initialized = initailize,
            SceneManager = sceneManager
        });
        
    }
 
}
