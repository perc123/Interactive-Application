using System;
using System.Collections;
using System.Collections.Generic;
using RocketManObject;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{

 
    
    public int count = 0;

    [SerializeField]
    GameObject question1;
    [SerializeField]
    GameObject question2;
    [SerializeField]
    GameObject question3;
    [SerializeField]
    GameObject creditMenu;
    
    [SerializeField] private AnswerBuilder answerBuilder;
    [SerializeField] private List<TMP_InputField> answerFields;

    public void IncreaseCounter()
    {

        if (count < 3) 
        {
            count++;
        }
    }

    public void DecreaseCounter()
    {

        if (count > 0)
        {
            count--;
        }

    }

    public void AddAnswer()
    {
        if (answerFields[count].text == "")
        {
            return;
        }
        answerBuilder.AddAnswer(answerFields[count].text);
        IncreaseCounter();
    }


    // Update is called once per frame
    void Update()
    {
        
        // Aktiviere die Frage basierend auf dem count Wert
        switch(count)
        {
            case 0:
                question1.SetActive(true);
                question2.SetActive(false);
                question3.SetActive(false);
                break;
            case 1:
                question1.SetActive(false);
                question2.SetActive(true);
                question3.SetActive(false);
                break;
            case 2:
                question1.SetActive(false);
                question2.SetActive(false);
                question3.SetActive(true);
                break;
            case 3:
                question1.SetActive(false);
                question2.SetActive(false);
                question3.SetActive(false);
                creditMenu.SetActive(true);
                break;
        }
    }

    private void OnEnable()
    {
        count = 0;
    }
}
