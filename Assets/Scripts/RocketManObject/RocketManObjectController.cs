using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketManObjectController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TMP_Text _answerText;
    
    private Sprite _avatar;
    private string _answer;
    private string _name;

    public void SetAvatarByName(string avatar)
    {
        Sprite avatarSprite = Resources.Load<Sprite>(avatar);
        _avatar = avatarSprite;
        _spriteRenderer.sprite = avatarSprite;
    }
    
    public void SetAvatar(Sprite avatar)
    {
        _avatar = avatar;
        _spriteRenderer.sprite = avatar;
    }
    
    public void SetAnswer(string answer)
    {
        _answer = answer;
        _answerText.text = answer;
    }

    public string GetAvatarName()
    {
        return _avatar.name;
    }
    
    public void SetName(string name)
    {
        _name = name;
    }
    
    public Sprite GetAvatar()
    {
        return _avatar;
    }
    
    public string GetAnswer()
    {
        return _answer;
    }
    
    public string GetName()
    {
        return _name;
    }
    
    
}
