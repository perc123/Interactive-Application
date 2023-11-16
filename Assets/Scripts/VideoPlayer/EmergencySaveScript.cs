using UnityEngine;

public class EmergencySaveScript : MonoBehaviour
{
    public GameObject videoPlayer;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.S))
        {
            videoPlayer.GetComponent<AnimationManager>().SaveLists();
            Application.Quit();
        }
    }
}
