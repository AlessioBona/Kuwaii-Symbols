using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (var playerToServer in FindObjectsOfType<PlayerToServer>())
        {
            if (playerToServer.isLocalPlayer)
            {
                playerToServer.CallForHelp(FindObjectOfType<AnswersScript>().rightAnswer);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AskForHelp()
    {

    }
}
