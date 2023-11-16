using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPlayer
{
    public class ImageButtonCreator : MonoBehaviour
    {
        public GameObject imageButtonPrefab; // The prefab of the Image Button to create.
        
        public string spriteFolderPath;

        void Start()
        {
            // Get the file paths of all sprites in the folder.
            string[] spriteFolderPaths = Directory.GetFiles(spriteFolderPath, "*.png");

            // Create Image Buttons for each prefab.
            foreach (string filePath in spriteFolderPaths)
            {
                CreateImageButton(filePath);
            }
            imageButtonPrefab.SetActive(false);
        }

        void CreateImageButton(string spritePath)
        {
            // Load the sprite as Texture2D
            Texture2D texture = LoadTextureFromFile(spritePath);
            // Create a Sprite from the Texture2D.
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            // Instantiate a new Image Button from the prefab.
            GameObject imageButtonObj = Instantiate(imageButtonPrefab, transform);

            // Get the Image component of the Image Button.
            Image image = imageButtonObj.GetComponent<Image>();

            // Set the sprite of the Image component to display the prefab's thumbnail or custom image.
            image.sprite = sprite;
            imageButtonObj.GetComponent<SpawnSprite>().fileName = Path.GetFileNameWithoutExtension(spritePath);
        }

        Texture2D LoadTextureFromFile(string filePath)
        {
            // Read the bytes from the file.
            byte[] bytes = File.ReadAllBytes(filePath);

            // Create a new Texture2D.
            Texture2D texture = new Texture2D(2, 2);

            // Load the image data into the Texture2D.
            texture.LoadImage(bytes);

            return texture;
        }
    }
}