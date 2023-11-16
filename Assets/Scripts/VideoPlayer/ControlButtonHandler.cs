using System;
using UnityEngine;
using UnityEngine.UI;

public class ControlButtonHandler : MonoBehaviour
{
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button pauseButton;
    
    // Private Variablen für die Button-Farben
    private Color normalColor;
    private Color selectedColor;

    public void Start()
    {
        normalColor = playButton.colors.normalColor;
        selectedColor = Color.grey;
    }

    public void playButtonClick()
    {
        ResetButtonColors(pauseButton);
        SetSelectedButtonColors(playButton);
    }
    
    public void pauseButtonClick()
    {
        ResetButtonColors(playButton);
        SetSelectedButtonColors(pauseButton);
    }
    
    void ResetButtonColors(Button button)
    {
        ColorBlock buttonColors = button.colors;
        buttonColors.normalColor = normalColor; 
        buttonColors.highlightedColor = normalColor;
        buttonColors.pressedColor = normalColor;
        buttonColors.selectedColor = normalColor;
        button.colors = buttonColors;
    }

    // Methode zum Setzen der Farben eines ausgewählten Buttons auf die ausgewählte Farbe
    void SetSelectedButtonColors(Button button)
    {
        ColorBlock buttonColors = button.colors;
        buttonColors.normalColor = selectedColor;
        buttonColors.highlightedColor = selectedColor;
        buttonColors.pressedColor = selectedColor;
        buttonColors.selectedColor = selectedColor;
        button.colors = buttonColors;
    }
}
