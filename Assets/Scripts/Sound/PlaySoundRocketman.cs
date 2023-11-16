using UnityEngine;

/*
Das PlaySoundRocketman-Skript ist dafür verantwortlich, den Song "Rocketman" abzuspielen, wenn das MainMenu-GameObject 
deaktiviert und das scrollViewRocket-GameObject aktiviert ist.

Die Update()-Methode wird einmal pro Frame aufgerufen. Sie überprüft, ob das MainMenu deaktiviert ist, scrollViewRocket aktiviert 
ist, und ob der Song entweder noch nicht abgespielt wurde oder der aktuelle Musikindex in MusicStateManager nicht dem musicIndex 
(0 für "Rocketman") entspricht. Wenn diese Bedingungen erfüllt sind, wird der Musik-Track mit dem Index 0 abgespielt, der Song-Status 
in MusicStateManager wird auf "abgespielt" gesetzt, und der aktuelle Musikindex wird aktualisiert.

Wenn das MainMenu aktiviert ist, wird der Song-Status in MusicStateManager auf "nicht abgespielt" gesetzt. 
Dies ermöglicht das erneute Abspielen des Songs, wenn die Bedingungen in der Update()-Methode erneut erfüllt sind.
 */

public class PlaySoundRocketman : MonoBehaviour
{
    // Referenz zum MainMenu-GameObject
    public GameObject MainMenu;

    // Referenz zum scrollViewRocket-GameObject
    public GameObject scrollViewRocket;

    // Musikindex, der dem Song "Rocketman" entspricht
    private int musicIndex = 0;

    // Update-Methode wird einmal pro Frame aufgerufen
    void Update()
    {
        // Überprüft, ob das MainMenu deaktiviert ist und scrollViewRocket aktiviert ist,
        // und ob der Song noch nicht abgespielt wurde oder der aktuelle Musikindex nicht dem musicIndex entspricht
        if (MainMenu.activeSelf.Equals(false) && scrollViewRocket.activeSelf.Equals(true) && (!MusicStateManager.instance.songPlayed || MusicStateManager.instance.currentMusicIndex != musicIndex))
        {
            // Spielt den Musik-Track mit dem Index 0 (Rocketman) ab
            MusicManager.instance.PlayMusic(musicIndex);

            // Setzt den Song-Status in MusicStateManager auf "abgespielt"
            MusicStateManager.instance.songPlayed = true;

            // Aktualisiert den aktuellen Musikindex in MusicStateManager
            MusicStateManager.instance.currentMusicIndex = musicIndex;
        }
        // Wenn das MainMenu aktiviert ist
        else if (MainMenu.activeSelf.Equals(true))
        {
            // Setzt den Song-Status in MusicStateManager auf "nicht abgespielt"
            MusicStateManager.instance.songPlayed = false;
        }
    }
}
