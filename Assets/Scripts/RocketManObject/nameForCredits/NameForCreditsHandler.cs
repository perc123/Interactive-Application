using System.Collections;
using System.Collections.Generic;
using RocketManObject;
using TMPro;
using UnityEngine;

public class NameForCreditsHandler : MonoBehaviour
{
    
    [SerializeField] private AnswerBuilder answerBuilder;
    [SerializeField] private TMP_InputField nameForCreditsRocket;
    [SerializeField] private TMP_InputField nameForCreditsQueen;
    
    public void SetNameForCreditsQueen()
    {
        answerBuilder.SetName(nameForCreditsQueen.text);
        nameForCreditsQueen.text = "";
    }
    
    public void SetNameForCreditsRocket()
    {
        answerBuilder.SetName(nameForCreditsRocket.text);
        nameForCreditsRocket.text = "";
    }
}
