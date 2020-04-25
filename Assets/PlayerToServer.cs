using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerToServer : NetworkBehaviour
{
    //[SyncVar]
    //public int prova;

    public GameObject helpRequestPrefab;

    public void Update()
    {
        if (isClient && hasAuthority)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.LogError("Sending a message!!");
                // send to server
                CmdHelpServer(netId, 99);
            }
        }
    }

    public void CallForHelp(int helpId)
    {
        CmdHelpServer(netId, helpId);
    }

    public void AnswerFound()
    {
        CmdHelpServer(netId, 99);
    }

    [Command]
    public void CmdAddGlobalScore()
    {
        FindObjectOfType<GameServer>().AddGlobalScore();
    }

    [Command]
    public void CmdHelpServer(NetworkInstanceId clientNetId, int request)
    {
        FindObjectOfType<GameServer>().HelpRequestFromClient(clientNetId, request);
    }

    public void HelpRequestFromServer(NetworkInstanceId clientNetId, int request)
    {
        var itsAMe = this.netId == clientNetId;
        //Debug.LogError(" > " + message + (itsAMe ? " (That's from me!!)" : ""));
        //Debug.LogError("   this.netId == " + this.netId);
        //Debug.LogError("   clientNetId == " + this.netId);
        if (!itsAMe)
        {
            if(request == 99)
            {
                OneHelpRequest[] requests = FindObjectsOfType<OneHelpRequest>();
                foreach(OneHelpRequest req in requests)
                {
                    if(req.playerID == clientNetId)
                    {
                        GameObject.Destroy(req.gameObject);
                    }
                }
                // get back
            } else
            {
                GameObject newHR = Instantiate(helpRequestPrefab,FindObjectOfType<HelpRequestManager>().transform);
                newHR.GetComponent<OneHelpRequest>().playerID = clientNetId;
                newHR.GetComponent<OneHelpRequest>().requestID = request;
                newHR.GetComponent<OneHelpRequest>().title.text = "Player n." + clientNetId + " asks:";
                newHR.GetComponent<OneHelpRequest>().button.text = FindObjectOfType<HanziListHolder>().list[request];
                // put request for Hanzi n. Request
            }


        }
    }

    //[TargetRpc]
}
