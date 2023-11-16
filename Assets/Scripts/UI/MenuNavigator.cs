using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuNavigator : MonoBehaviour
{
    public Button nextButton;
    public Button backButton;
    public Button skipButton;
    public Canvas[] questionMenus;

    private int currentMenuIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        currentMenuIndex = 0;
        ShowCurrentMenu();

        nextButton.onClick.AddListener(NextButtonClicked);
        backButton.onClick.AddListener(BackButtonClicked);
        skipButton.onClick.AddListener(SkipButtonClicked);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SkipButtonClicked()
    {
        currentMenuIndex = 0;
        ShowCurrentMenu();
    }

    private void NextButtonClicked()
    {
        currentMenuIndex++;
        if (currentMenuIndex >= questionMenus.Length)
            currentMenuIndex = 0;
       else if (currentMenuIndex == 0)
            currentMenuIndex = 1;

        ShowCurrentMenu();
    }
    
    private void BackButtonClicked()
    {
        currentMenuIndex--;
        if (currentMenuIndex < 0)
            currentMenuIndex = 0;

        ShowCurrentMenu();
    }

    private void ShowCurrentMenu()
    {
        for (int i = 0; i < questionMenus.Length; i++)
        {
            if (i == currentMenuIndex)
                questionMenus[i].gameObject.SetActive(true);
            else
            {
                questionMenus[i].gameObject.SetActive(false);
            }
        }
    }
}
