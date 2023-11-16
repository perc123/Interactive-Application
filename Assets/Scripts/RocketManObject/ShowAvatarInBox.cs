using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAvatarInBox : MonoBehaviour
{
    // Create field to insert target GameObject that will display the avatar
    [SerializeField] private Image targetImage;

    // Method taking image of clicked button and sets it to the image displayed in the middle of the screen
    public void OnButtonClick(Button clickedButton)
    {
        Image clickedImage = clickedButton.GetComponent<Image>();
        targetImage.sprite = clickedImage.sprite;
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
