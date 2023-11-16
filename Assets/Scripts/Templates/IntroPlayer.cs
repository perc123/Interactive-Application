using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IntroPlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float timeBetweenSprites = 0.2f;

    // if set to -1 never loop otherwise restart loop from this index    
    [SerializeField] private int restartLoopFrom = -1;

    private int currentSpriteIndex = 0;
    private float timeSinceLastSprite = 0;
    private bool isPlaying = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlaying = !isPlaying;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isPlaying)
        {
            currentSpriteIndex = 0;
            spriteRenderer.sprite = null;
            return;
        }

        timeSinceLastSprite += Time.deltaTime;
        if (timeSinceLastSprite >= timeBetweenSprites)
        {
            timeSinceLastSprite = 0;
            if (currentSpriteIndex >= sprites.Count)
            {
                if (restartLoopFrom != -1)
                {
                    currentSpriteIndex = restartLoopFrom;
                    return;
                }

                ResetIntro();
                isPlaying = false;
                return;
            }

            spriteRenderer.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex++;
        }
    }

    public void ResetIntro()
    {
        currentSpriteIndex = 0;
        spriteRenderer.sprite = null;
    }
}