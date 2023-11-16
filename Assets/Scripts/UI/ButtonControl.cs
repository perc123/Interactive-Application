using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public Button yourButton; // Ihr Button
    public TMP_InputField yourInputField; // Ihr Input-Feld

    // In der Update-Methode prüfen wir, ob der Text im Input-Feld leer ist
    void Update()
    {
        if (string.IsNullOrEmpty(yourInputField.text))
        {
            yourButton.interactable = false; // Button deaktivieren
        }
        else
        {
            yourButton.interactable = true; // Button aktivieren
        }
    }
}
