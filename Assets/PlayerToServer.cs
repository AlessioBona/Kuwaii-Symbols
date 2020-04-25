using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerToServer : NetworkBehaviour
{
    //[SyncVar]
    //public int prova;

    public void Update()
    {
        if (isClient && hasAuthority)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.LogError("Sending a message!!");
                // send to server
                CmdHelloServer(netId, "Hello " + Time.frameCount + " from " + netId);
            }
        }
    }

    [Command]
    public void CmdAddGlobalScore()
    {
        FindObjectOfType<GameServer>().AddGlobalScore();
    }

    [Command]
    public void CmdHelloServer(NetworkInstanceId clientNetId, string message)
    {
        FindObjectOfType<GameServer>().HelloFromClient(clientNetId, message);
    }

    public void HelloFromServer(NetworkInstanceId clientNetId, string message)
    {
        var itsAMe = this.netId == clientNetId;
        Debug.LogError(" > " + message + (itsAMe ? " (That's from me!!)" : ""));
        Debug.LogError("   this.netId == " + this.netId);
        Debug.LogError("   clientNetId == " + this.netId);
    }

    //[TargetRpc]
}
