using UnityEngine;

public class StopSound : MonoBehaviour
{
    // Referenz zum MainMenu-GameObject
    public GameObject mainMenu;

    // Update-Methode wird einmal pro Frame aufgerufen
    void Update()
    {
        // Überprüft, ob das MainMenu aktiviert ist
        if (mainMenu.activeSelf.Equals(true))
        {
            // Stoppt den aktuellen Musik-Track
                MusicManager.instance.GetComponent<AudioSource>().Stop();

            // Setzt den Song-Status auf "nicht abgespielt"
            MusicStateManager.instance.songPlayed = false;
        }
    }
}
