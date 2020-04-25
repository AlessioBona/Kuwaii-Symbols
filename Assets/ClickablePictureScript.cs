using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickablePictureScript : MonoBehaviour
{
    public int rightAnswer = 0;
    public GameObject good;
    public GameObject bad;
    public bool isKW;
    public AnswersScript myAnswerScript;

    public float KWTime = 30f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = KWTime;
        isKW = false;
        good.SetActive(false);
        bad.SetActive(true);
    }



    private void Update()
    {
        if (isKW)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            timer = KWTime;
            KawaiiOff();
        }
    }

    public void ClickedOnMe()
    {
        if (!isKW)
        {
            // AnswersScript myAnswerScript = FindObjectOfType<AnswersScript>();
            myAnswerScript.gameObject.SetActive(true);
            myAnswerScript.rightAnswer = rightAnswer;
            myAnswerScript.SetAnswers();
        }
    }

    public void KawaiiOn()
    {
        isKW = true;
        good.SetActive(true);
        bad.SetActive(false);
    }

    public void KawaiiOff()
    {
        isKW = false;
        good.SetActive(false);
        bad.SetActive(true);
    }

}
