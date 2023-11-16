using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearInputField : MonoBehaviour
{
    public Button yourButton; // Ihr Button
    public TMP_InputField yourInputField; // Ihr Input-Feld
    
    void Start()
    {
        // Fügen Sie dem Button eine Funktion hinzu, die aufgerufen wird, wenn der Button geklickt wird
        yourButton.onClick.AddListener(ClearField);
    }

    // Diese Methode wird aufgerufen, wenn der Button geklickt wird
    void ClearField()
    {
        yourInputField.text = string.Empty; // Text des Input-Feldes löschen
    }
}
