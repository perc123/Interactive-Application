using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Das MusicManager-Skript ist ein Singleton, das dazu verwendet wird, Musik im Unity-Projekt zu verwalten. 
 Es enthält ein Array von AudioClip-Objekten, die die Musik-Tracks repräsentieren. Eine AudioSource-Komponente ist 
 für das Abspielen der Musik verantwortlich. Die Awake()-Methode stellt sicher, dass nur eine Instanz des MusicManager 
 in der Szene vorhanden ist, und weist die AudioSource-Komponente der audioSource-Variablen zu.
 
 Die PlayMusic()-Methode spielt einen Musik-Track basierend auf dem übergebenen Index (trackIndex) ab. Zuerst überprüft sie, 
 ob der Index gültig ist. Wenn der Index ungültig ist, gibt sie eine Warnmeldung aus und beendet die Methode. Andernfalls stoppt 
 sie die aktuelle Wiedergabe, wenn die AudioSource bereits Musik abspielt, und weist dann den entsprechenden AudioClip aus dem 
 musicTracks-Array basierend auf dem trackIndex der AudioSource-Komponente zu. Schließlich beginnt sie die Wiedergabe des 
 zugewiesenen AudioClip.
 */

public class MusicManager : MonoBehaviour
{
    // Singleton-Instanz, um auf den MusicManager von anderen Skripten aus zuzugreifen
    public static MusicManager instance;

    // Array, das alle Musik-Clips enthält
    public AudioClip[] musicTracks;

    // AudioSource-Komponente, die zum Abspielen von Musik verwendet wird
    private AudioSource audioSource;

    // Awake-Methode wird beim Laden des Skripts aufgerufen
    private void Awake()
    {
        // Singleton-Pattern: Stellt sicher, dass nur eine Instanz des MusicManagers in der Szene existiert
        if (instance == null)
        {
            instance = this;
            // Verhindert das Zerstören des GameObjects, wenn eine neue Szene geladen wird
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Zerstört das GameObject, wenn bereits eine Instanz des MusicManagers existiert
            Destroy(gameObject);
            return;
        }

        // AudioSource-Komponente wird der Variablen audioSource zugewiesen
        audioSource = GetComponent<AudioSource>();
    }

    // PlayMusic-Methode, die einen Musik-Clip basierend auf dem angegebenen Index abspielt
    public void PlayMusic(int trackIndex)
    {
        // Überprüft, ob der angegebene trackIndex gültig ist
        if (trackIndex < 0 || trackIndex >= musicTracks.Length)
        {
            // Gibt eine Warnmeldung aus, wenn der trackIndex ungültig ist
            Debug.LogWarning("Invalid track index.");
            return;
        }

        // Stoppt die Wiedergabe, wenn die AudioSource bereits Musik abspielt
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Weist den AudioClip aus dem musicTracks-Array basierend auf dem trackIndex der AudioSource-Komponente zu
        audioSource.clip = musicTracks[trackIndex];
        
        // Beginnt die Wiedergabe des zugewiesenen AudioClip
        audioSource.Play();
    }
}
