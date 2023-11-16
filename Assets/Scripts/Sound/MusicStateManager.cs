using UnityEngine;

/*
Der MusicStateManager ist ein Singleton-Script, das den Zustand der Musik im Unity-Projekt verwaltet. Es verfolgt, 
ob ein Song gerade abgespielt wird (songPlayed) und den aktuellen Index des abgespielten Musik-Tracks (currentMusicIndex).

Die Awake()-Methode stellt sicher, dass nur eine Instanz des MusicStateManager in der Szene existiert, 
indem sie das Singleton-Pattern implementiert. Wenn keine Instanz des MusicStateManager vorhanden ist,
wird die aktuelle Instanz als Singleton-Instanz festgelegt und das GameObject wird nicht zerstört, 
wenn eine neue Szene geladen wird. Wenn bereits eine Instanz des MusicStateManager existiert, wird das GameObject zerstört.
Mit diesem Skript können andere Skripte auf die instance-Variable zugreifen, um den Zustand der Musik in deinem Projekt zu verwalten.
 */

public class MusicStateManager : MonoBehaviour
{
    // Singleton-Instanz, um auf den MusicStateManager von anderen Skripten aus zuzugreifen
    public static MusicStateManager instance;

    // Variable, die den aktuellen Abspielzustand eines Songs speichert (wahr, wenn ein Song abgespielt wird)
    public bool songPlayed;

    // Variable, die den aktuellen Musik-Index speichert, um den aktuellen abgespielten Musik-Track zu verfolgen
    public int currentMusicIndex;

    // Awake-Methode wird beim Laden des Skripts aufgerufen
    private void Awake()
    {
        // Singleton-Pattern: Stellt sicher, dass nur eine Instanz des MusicStateManagers in der Szene existiert
        if (instance == null)
        {
            instance = this;
            // Verhindert das Zerstören des GameObjects, wenn eine neue Szene geladen wird
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Zerstört das GameObject, wenn bereits eine Instanz des MusicStateManagers existiert
            Destroy(gameObject);
            return;
        }
    }
}
