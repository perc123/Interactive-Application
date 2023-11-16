using UnityEngine;
using UnityEngine.UI;

/*
Das Skript ViewportSwitcher ermöglicht das Umschalten zwischen drei ScrollViews in einer Unity-Szene, 
indem auf jeweils zugeordnete Buttons geklickt wird. Beim Klicken auf einen Button wird die zugehörige 
ScrollView aktiviert und die anderen deaktiviert. Gleichzeitig wird die Farbe des ausgewählten Buttons 
geändert, während die anderen Buttons ihre normale Farbe behalten.

Das Skript besteht aus einer Start-Methode, die beim Starten der Anwendung aufgerufen wird, und einer 
SwitchViewports-Methode, die zum Umschalten der Viewports und zum Hervorheben des ausgewählten Buttons 
verwendet wird. Zusätzlich gibt es zwei Hilfsmethoden: ResetButtonColors und SetSelectedButtonColors.

In der Start-Methode werden die normalen und ausgewählten Button-Farben gespeichert, den Buttons 
onClick-Events hinzugefügt, die die SwitchViewports-Methode aufrufen, und die erste ScrollView aktiviert, 
während die anderen deaktiviert werden.

Die SwitchViewports-Methode nimmt zwei Parameter: viewportToShow, der das GameObject der anzuzeigenden 
ScrollView enthält, und selectedButton, der den ausgewählten Button repräsentiert. Innerhalb dieser 
Methode werden zunächst alle ScrollViews deaktiviert, dann wird die gewünschte ScrollView aktiviert. 
Anschließend werden die Farben aller Buttons mit der ResetButtonColors-Methode auf die normale Farbe 
zurückgesetzt, und für den ausgewählten Button wird die Farbe mithilfe der SetSelectedButtonColors-Methode 
auf die ausgewählte Farbe geändert.

Die Hilfsmethode ResetButtonColors setzt die Farben eines Buttons auf die normale Farbe zurück, während 
die SetSelectedButtonColors-Methode die Farben eines Buttons auf die ausgewählte Farbe setzt. Dadurch 
wird das Hervorheben der ausgewählten Buttons ermöglicht.

Das Skript muss einem GameObject in der Unity-Szene hinzugefügt werden und benötigt Referenzen zu den 
drei Button-Objekten und den drei ScrollView-Objekten. Diese Referenzen müssen im Unity-Inspektor 
zugewiesen werden. Sobald alles eingerichtet ist, können Sie Ihre Anwendung ausführen, und das Skript 
wird das Umschalten zwischen den ScrollViews und das Hervorheben der ausgewählten Buttons ermöglichen.
 */
public class ViewportSwitcher : MonoBehaviour
{
    // Öffentliche Variablen für die Buttons und die ScrollViews
    public Button button1;
    public Button button2;
    public GameObject scrollView1;
    public GameObject scrollView2;

    // Private Variablen für die Button-Farben
    private Color normalColor;
    private Color selectedColor;

    // Diese Methode wird beim Start des Spiels aufgerufen
    void Start()
    {
        Debug.Log("ViewportSwitcher started");
        // Speichere die Farben für normale und ausgewählte Buttons
        normalColor = button1.colors.normalColor;
        selectedColor = Color.grey;

        // Füge den Buttons onClick-Events hinzu, die die SwitchViewports-Methode aufrufen
        button1.onClick.AddListener(() => SwitchViewports(scrollView1, button1));
        button2.onClick.AddListener(() => SwitchViewports(scrollView2, button2));

        // Aktiviere die erste ScrollView und deaktiviere die anderen beim Start
        SwitchViewports(scrollView2, button2);
    }

    // Methode zum Umschalten der Viewports und Hervorheben des ausgewählten Buttons
    void SwitchViewports(GameObject viewportToShow, Button selectedButton)
    {
        Debug.Log("Switching viewports");
        // Deaktiviere alle ScrollViews
        scrollView1.SetActive(false);
        scrollView2.SetActive(false);

        // Aktiviere die ScrollView, die angezeigt werden soll
        viewportToShow.SetActive(true);

        // Setze die Farben aller Buttons auf die normale Farbe
        ResetButtonColors(button1);
        ResetButtonColors(button2);

        // Setze die Farben des ausgewählten Buttons auf die ausgewählte Farbe
        SetSelectedButtonColors(selectedButton);
    }

    // Methode zum Zurücksetzen der Farben eines Buttons auf die normale Farbe
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
