using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateRandomQuestion : MonoBehaviour
{
    // Define array of InputFields and array of Questions
    public TMP_InputField[] randomQuestionFields;
    string[] setOfQuestions = new string[]{"I am proud of......", "I regret....", "I dream of...", "I wish I could.......", "If I could I would.....", "I am afraid of....", "I feel most alone when.....", "If I had one wish, I would wish for.....", "I am greatful for....", "I am inspired by.....", "I believe in....", "I want to share....", "I love.... (doing, person etc.)", "I am guilty of...."};

        
    // List to keep overview of already generated numbers
    private List<int> generatedRandomNumber = new List<int>();
        
        
    // Start is called before the first frame update
    void Start()
        {
            SetText();
        }

    // Creates a void that we can call in one of our start that will set the text/question
    public void SetText() 
    {
        

        // Generate unique random numbers from getNoNumberTwice() method
        int randomNumber1 = getNoNumberTwice();
        int randomNumber2 = getNoNumberTwice();
        int randomNumber3 = getNoNumberTwice();

        // Take numbers and convert to question selected
        string textToPutToInputField1 = setOfQuestions[randomNumber1];
        string textToPutToInputField2 = setOfQuestions[randomNumber2];
        string textToPutToInputField3 = setOfQuestions[randomNumber3];

        // Sets Input Field to random question selected
        randomQuestionFields[0].text = textToPutToInputField1;
        randomQuestionFields[1].text = textToPutToInputField2;
        randomQuestionFields[2].text = textToPutToInputField3;
    }

    // Method to generate unique random Numbers. Random number is generated from Random. and checked against a list of already generated random numbers as we add each number to the list. 
    // 
    private int getNoNumberTwice()
    {
        int uniqueRandomNumber;
        
            do
            {
                uniqueRandomNumber = Random.Range(0, setOfQuestions.Length);
            } while (generatedRandomNumber.Contains(uniqueRandomNumber)) ;

            generatedRandomNumber.Add(uniqueRandomNumber);
            return uniqueRandomNumber;
    }
}
