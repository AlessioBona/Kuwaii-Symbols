using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButtonScript : MonoBehaviour
{
    public int answer = 99;

    public void BeenClicked()
    {
        GetComponentInParent<AnswersScript>().AttemptToAnswer(answer);
    }
    
}
