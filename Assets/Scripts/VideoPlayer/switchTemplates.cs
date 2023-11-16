using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class switchTemplates : MonoBehaviour
{
    
    public int counter = 0;
    
    [SerializeField] private Canvas canvas;

    [SerializeField] private SpriteRenderer videoPlayer;
    [SerializeField] private GameObject UI_RocketManBackground;
    
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();
    
    [SerializeField] private GameObject cutouts;
    
    [SerializeField] private List<string> templateNames = new List<string>();
    [SerializeField] private TextMeshProUGUI templateNameText;
    
    private bool canvasActive = true;

    [SerializeField]
    GameObject queenGameObject;
    
    [SerializeField]
    GameObject rocketGameObject;
    
    [SerializeField]
    private IntroPlayer introPlayer;

    public AnimationManager animationManager;
    void Update()
    {
        
        if (counter == 5)
        {
            animationManager.currentSong = Song.Queen;
        }

        if (counter == 25 || counter == 24)
        {
            animationManager.currentSong = Song.RocketMan;
            UI_RocketManBackground.SetActive(true);
        }
        else
        {
            UI_RocketManBackground.SetActive(false);
        }
        

        bool hasChanged = false;
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.V))
        {
            counter = 0;
            //videoPlayer.color = Color.clear;
            videoPlayer.color = Color.black;
            for (int i = 0; i < gameObjects.Count; i++) 
            {
                gameObjects[i].SetActive(false);
            }
            if (canvasActive)
            {
                videoPlayer.color = new Color(videoPlayer.color.r, videoPlayer.color.g, videoPlayer.color.b, 0);
                gameObjects[0].SetActive(true);
                cutouts.SetActive(true);
                canvasActive = false;
            }
            else
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].SetActive(false);
                }
                gameObjects[5].SetActive(true);
                cutouts.SetActive(false);
                videoPlayer.color=Color.white;
                canvasActive = true;
            }
            
        }

        // Increase the counter if the right arrow key is pressed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (canvas.isActiveAndEnabled) return;

                counter = Mathf.Min(counter + 1, gameObjects.Count - 1);
            animationManager.StopTimer();
            introPlayer.ResetIntro();
            if(counter == 5)
            {
                videoPlayer.color = Color.black;
            }
            else
            {
                videoPlayer.color = new Color(videoPlayer.color.r, videoPlayer.color.g, videoPlayer.color.b, 0);
            }
            hasChanged = true;
        }

        // Decrease the counter if the left arrow key is pressed
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (canvas.isActiveAndEnabled) return;

                counter = Mathf.Max(counter - 1, 0);
            animationManager.StopTimer();
            introPlayer.ResetIntro();
            if(counter == 5)
            {
                videoPlayer.color = Color.black;
            }
            else
            {
                videoPlayer.color = new Color(videoPlayer.color.r, videoPlayer.color.g, videoPlayer.color.b, 0);
            }
            hasChanged = true;
        }

        // Update visibility if counter has changed.
        if(hasChanged)
        {
            UpdateVisibility();
        }
    }

    void UpdateVisibility()
    {
        // First, set all GameObjects to inactive
        for(int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].SetActive(false);
        }

        // Then, set only the GameObject at the counter index to active
        if (counter < gameObjects.Count)
        {
            templateNameText.text = templateNames[counter];
            gameObjects[counter].SetActive(true);
        }
    }
    

}
