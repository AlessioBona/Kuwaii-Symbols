using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameServer : NetworkBehaviour
{
    [SyncVar(hook ="OnGlobalScoreChange")]
    public int globalScore;

    public void AddGlobalScore()
    {
        this.globalScore++;
    }

    public void OnGlobalScoreChange(int newGlobalScore)
    {
        // newGlobalScore = new value
        // globalScore = still old value until this function terminates

        // Not necessary, will implicitly be called after this function: globalScore = newGlobalScore;
    }

    public void HelloFromClient(NetworkInstanceId clientNetId, string message)
    {
        Debug.LogError(" > " + message);

        RpcHelloClient(clientNetId, "A client once said to me: " + message);
    }

    [ClientRpc]
    public void RpcHelloClient(NetworkInstanceId clientNetId, string message)
    {
        foreach (var playerToServer in FindObjectsOfType<PlayerToServer>()) {
            if (playerToServer.isLocalPlayer)
            {
                playerToServer.HelloFromServer(clientNetId, message);
            }
        }
    }
}
