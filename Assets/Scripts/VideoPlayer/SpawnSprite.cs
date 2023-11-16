using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class SpawnSprite : MonoBehaviour
{
    public string filePath;
    public string fileName;
    public GameObject objectToSpawn;
    
    public Song song;
    
    [SerializeField] private GameObject videoPlayer;
    [SerializeField] private AnimationManager animationManager;


    public void Start()
    {
        filePath = Path.Combine(filePath, fileName); //Dateipfad vom Prefab finden
    }

    public void OnButtonClick()
    {
        Debug.Log("adding Object");
        // Erstelle eine Instanz des Prefabs aus dem Ordner
        GameObject prefab = Resources.Load<GameObject>(filePath);
        GameObject newGameObject = Instantiate(objectToSpawn, new Vector3(-3.3f,1.5f,0), Quaternion.identity);
        

        // find the video player object
        videoPlayer = GameObject.Find("Videoplayer");
        animationManager = videoPlayer.GetComponent<AnimationManager>();
        AnimationHandler animationHandler = newGameObject.GetComponent<AnimationHandler>();

        if (animationManager.currentSong == Song.Queen)
        {
            if (animationManager.time == 0)
            {
                animationHandler.SetStartTime(animationManager.time + 0.01f);
                animationManager.time = animationManager.time + 0.01f;
            }
            else
            {
                animationHandler.SetStartTime(animationManager.time);
            }
            
        }
        
        animationHandler.isFromCurrentSession = true;
        
        animationManager.AddObject(newGameObject, song);

        switch (song)
        {
            case Song.Queen:
                var parentQueen = GameObject.Find("QueenObjects");
                
                Debug.Log("parentQueen: " + parentQueen);
                Debug.Log("newGameObject: " + newGameObject);
                newGameObject.transform.parent = parentQueen.transform;
                break;
            case Song.RocketMan:
                var parentRocketMan = GameObject.Find("RocketManObjects");
                newGameObject.transform.parent = parentRocketMan.transform;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
    }




}

public enum Song
{
    Queen, RocketMan
}
