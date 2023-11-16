using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollViewsController : MonoBehaviour
{
    public ScrollRect QueenScrollView;
    public ScrollRect RocketManScrollView;
    
    [SerializeField] private Song currentSong;
    
    // Update is called once per frame
    void Update()
    {
        switch (currentSong)
        {
            case Song.Queen:
                QueenScrollView.gameObject.SetActive(true);
                RocketManScrollView.gameObject.SetActive(false);
                break;
            case Song.RocketMan:
                QueenScrollView.gameObject.SetActive(false);
                RocketManScrollView.gameObject.SetActive(true);
                break;
        }
    }
    
    public void SetCurrentSong(int song)
    {
        currentSong = (Song) song;
    }
}
