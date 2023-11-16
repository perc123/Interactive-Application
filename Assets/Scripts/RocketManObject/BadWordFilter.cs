using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BadWordFilter : MonoBehaviour
{
    // Create InputField that will be checked and lists for words to be checked with
    public TMP_InputField inputField;
    public List<string> badWordsList1 = new List<string>();
    public List<string> badWordsList2 = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        LoadWordList("de.txt", badWordsList1);
        LoadWordList("en.txt", badWordsList2);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Methode to load the list of bad words and 
    void LoadWordList(string listName, List<string> wordList)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(listName);
        if (textAsset != null)
        {
            string[] badWords = textAsset.text.Split(new char[] { '\r', '\n' });
            // filter out empty lines
            foreach (string word in badWords)
            {
                if (word.Length > 0)
                {
                    wordList.Add(word);
                }
            }
        }
    }

    // Checks if the text from the input field is contained in one of the lists
    public void checkForBadWords()
    {
        //check if any part of the input is in the bad word list\
        string[] inputWords = inputField.text.Split(new char[] { ' ' });
        foreach (string word in inputWords)
        {
            if (containsBadWord(word))
            {
                inputField.text = "";
            }
        }
    }

    private bool containsBadWord(string word)
    {
        if (badWordsList1.Exists(word.Contains) || badWordsList2.Exists(word.Contains))
        {
            return true;
        }
        return false;
    }
    
}