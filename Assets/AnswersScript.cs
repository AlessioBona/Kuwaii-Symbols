using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswersScript : MonoBehaviour
{
    public int rightAnswer = 99;
    public string[] hanziList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        hanziList = FindObjectOfType<HanziListHolder>().list;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnswers()
    {
        AnswerButtonScript[] buttons = FindObjectsOfType<AnswerButtonScript>();
        List<int> answers = new List<int>();
        answers.Add(rightAnswer);
        for (int i = 1; i < buttons.Length; i++)
        {
            int randomNumber = rightAnswer;
            while (answers.Contains(randomNumber)) { 
                randomNumber = Random.Range(0, hanziList.Length);
            }
            answers.Add(randomNumber);
        }
        ShuffleMe<int>(answers);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = hanziList[answers[i]];
            buttons[i].answer = answers[i];
        }

    }

    void ShuffleMe<T>(IList<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;

        for (int i = list.Count - 1; i > 1; i--)
        {
            int rnd = random.Next(i + 1);

            T value = list[rnd];
            list[rnd] = list[i];
            list[i] = value;
        }
    }

    public void CloseAnswers()
    {
        gameObject.SetActive(false);
    }

    public void AttemptToAnswer(int myAnswer)
    {
        if(myAnswer == rightAnswer)
        {
            ClickablePictureScript[] pictures = FindObjectsOfType<ClickablePictureScript>();
            foreach(ClickablePictureScript pic in pictures)
            {
                if(pic.rightAnswer == rightAnswer)
                {
                    pic.KawaiiOn();
                    foreach (var playerToServer in FindObjectsOfType<PlayerToServer>())
                    {
                        if (playerToServer.isLocalPlayer)
                        {
                            playerToServer.AnswerFound();
                        }
                    }
                    CloseAnswers();

                }
            }
        } else
        {
            // TIMER!!!
            CloseAnswers();
        }
    }
}
