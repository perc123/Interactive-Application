using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LyricsDisplayer : MonoBehaviour
{
    public float time; 
    public Song currentSong;
    
    public enum Song
    {
        Queen,
        RocketMan
    }
    
    [SerializeField] private TextMeshProUGUI _lyricsText;

    private void Start()
    {
        time = 0;
    }

    private void Update()
    {
        var lyrics = _lyricsData[currentSong].Item1;
        var timeStamps = _lyricsData[currentSong].Item2;

        // find lyric with closest timestamp to current time
        int index = -1;
        for (int i = 0; i < timeStamps.Count; i++)
        {
            if (timeStamps[i] <= time)
            {
                index = i;
            }
        }

        if (index != -1)
        {
            _lyricsText.text = lyrics[index];
        }
        
    }

    private Dictionary<Song, (List<string>, List<float>)> _lyricsData = new Dictionary<Song, (List<string>, List<float>)>()
    {
        {
            Song.Queen, 
            (
                new List<string>()
                {
                    "Tonight I′m gonna have myself a real good time",
                    "I feel alive ",
                    "and the world I'll turn it inside out - yeah",
                    "And floating around in ecstasy",
                    "So don′t stop me now don't stop me",
                    "'Cause I′m having a good time having a good time",
                    "I′m a shooting star leaping through the sky",
                    "Like a tiger defying the laws of gravity",
                    "I'm a racing car passing by like Lady Godiva",
                    "I′m gonna go go gon There's no stopping me",
                    "I′m burnin' through the sky yeah",
                    "Two hundred degrees That′s why they call me Mister Fahrenheit",
                    "I'm trav'ling at the speed of light",
                    "I wanna make a supersonic man out of you",
                    "Don′t stop me now I′m having such a good time",
                    "I'm having a ball (Don′t stop me now)",
                    "If you wanna have a good time just give me a call",
                    "Don't stop me now (′Cause I'm having a good time)",
                    "Don′t stop me now (Yes I'm havin' a good time)",
                    "I don′t want to stop at all",
                    "Yeah, I′m a rocket ship on my way to Mars",
                    "On a collision course",
                    "I am a satellite I'm out of control",
                    "I am a sex machine ready to reload",
                    "Like an atom bomb about to",
                    "Oh oh oh oh oh explode",
                    "I′m burnin' through the sky yeah",
                    "Two hundred degrees",
                    "That′s why they call me Mister Fahrenheit",
                    "I'm trav′ling at the speed of light",
                    "I wanna make a supersonic woman of you",
                    "Don't stop me don't stop me",
                    "Don′t stop me hey hey hey",
                    "Don′t stop me don't stop me",
                    "Ooh ooh ooh, I like it",
                    "Don′t stop me don't stop me",
                    "Have a good time good time",
                    "Don′t stop me don't stop me ah",
                    "Oh yeah",
                    "Alright",
                    "...",
                    "Oh, I′m burnin' through the sky yeah",
                    "Two hundred degrees",
                    "That's why they call me Mister Fahrenheit",
                    "I′m trav′ling at the speed of light",
                    "I wanna make a supersonic man out of you",
                    "Don't stop me now I′m having such a good time",
                    "I'm having a ball",
                    "Don′t stop me now",
                    "If you wanna have a good time (wooh)",
                    "Just give me a call (alright)",
                    "Don't stop me now (′cause I'm having a good time - yeah yeah)",
                    "Don't stop me now (yes I′m havin′ a good time)",
                    "I don't want to stop at all",
                    "La da da da daah",
                    "Da da da haa",
                    "Ha da da ha ha haaa",
                    "Ha da daa ha da da aaa",
                    "Ooh ooh ooh"
                },
                new List<float>()
                {
                    0,8,12,20,26,32,35,38,43,48,52,56,
                    60,63,66,70,75,79,82,84,89,92,93,
                    97,100,102,105,109,111,115,116,
                    120,122,125,126,128,129,131,134,
                    135,137,148,153,155,158,160,163,
                    168,170,172,174,176,179,182,187,
                    191,193,198,207
                }
            )
        },
        {
            Song.RocketMan, 
            (
                new List<string>()
                {
                    "She packed my bags last night, pre-flight",
                    "Zero hour, 9 a.m.",
                    "And I′m gonna be high",
                    "As a kite by then",
                    "...",
                    "I miss the Earth so much, I miss my wife",
                    "It's lonely out in space",
                    "On such a timeless flight",
                    "...",
                    "And I think it′s gonna be a long, long time",
                    "'Til touchdown brings me 'round again to find",
                    "I′m not the man they think I am at home",
                    "Oh, no, no, no",
                    "I′m a rocket man",
                    "Rocket man, burning out his fuse up here alone",
                    "...",
                    "And I think it's gonna be a long, long time",
                    "′Til touchdown brings me 'round again to find",
                    "I′m not the man they think I am at home",
                    "Oh, no, no, no",
                    "I'm a rocket man",
                    "Rocket man, burning out his fuse up here alone",
                    "...",
                    "Mars ain′t the kind of place to raise your kids",
                    "In fact, it's cold as hell",
                    "And there's no one there to raise them",
                    "If you did",
                    "...",
                    "And all this science, I don′t understand",
                    "It′s just my job five days a week",
                    "A rocket man",
                    "A rocket man",
                    "...",
                    "And I think it's gonna be a long, long time",
                    "′Til touchdown brings me 'round again to find",
                    "I′m not the man they think I am at home",
                    "Oh, no, no, no",
                    "I'm a rocket man",
                    "Rocket man, burning out his fuse up here alone",
                    "...",
                    "And I think it′s gonna be a long, long time",
                    "'Til touchdown brings me round again to find",
                    "I'm not the man they think I am at home",
                    "Oh, no, no, no",
                    "I′m a rocket man",
                    "Rocket man, burning out his fuse up here alone",
                    "...",
                    "And I think it′s gonna be a long, long time",
                    "And I think it′s gonna be a long, long time",
                    "And I think it′s gonna be a long, long time",
                    "And I think it′s gonna be a long, long time",
                    "And I think it′s gonna be a long, long time",
                    "And I think it′s gonna be a long, long time",
                    "And I think it′s gonna be a long, long time",
                    "And I think it′s gonna be a long, long time" 
                },
                new List<float>()
                {
                    0,7,13,18,22,26,33,40,46,52,55,60,63,65,68,74,79,
                    81,85,88,91,95,100,105,111,118,123,127,131,137,142,
                    150,153,157,160,164,167,170,174,179,183,187,190,
                    193,195,199,205,207,213,220,227,233,239,246,452

                }
            )
        }
    };

    public void SetCurrentSong(int song)
    {
        switch (song)
        {
            case 0:
                currentSong = Song.Queen;
                time = 0;
                break;
            case 1:
                currentSong = Song.RocketMan;
                time = 0;
                break;
        }
    }
}