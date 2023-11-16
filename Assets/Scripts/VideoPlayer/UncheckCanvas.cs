using UnityEngine;
using UnityEngine.UI;

public class UncheckCanvas : MonoBehaviour
{
    public Canvas canvas;
    private bool canvasActive = true;
    [SerializeField]
    GameObject videoPlayer;
    [SerializeField] private SpriteRenderer blackBackroundForBeamer;
    [SerializeField] private AnimationManager animationManager;
    
    [SerializeField] private Camera tabletCamera;
    [SerializeField] private Camera leftBeamerCamera;
    [SerializeField] private Camera rightBeamerCamera;
    
    [SerializeField] private Canvas canvasForBeamer;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.V))
        {
            animationManager.changeLifespanRocketmanObjects();
            if (canvasActive)
            {
                blackBackroundForBeamer.color = Color.black;
                
                animationManager.activateAllGameobjekts();
                
                tabletCamera.transform.position = new Vector3(1f, 3.52f, -10f);
                leftBeamerCamera.transform.position = new Vector3(1f, 3.52f, -10f);
                rightBeamerCamera.transform.position = new Vector3(1f, 3.52f, -10f);
                
                canvas.gameObject.SetActive(false);
                canvasActive = false;
                
                tabletCamera.orthographicSize = 3.450364f;
                leftBeamerCamera.orthographicSize = 3.450364f;
                rightBeamerCamera.orthographicSize = 3.450364f;
                
                // durch das wechseln in den Vollbildmodus, wird das abspielen des Songs gestoppt
                MusicManager.instance.GetComponent<AudioSource>().Stop();
                animationManager.StopTimer();
                canvasForBeamer.gameObject.SetActive(true);
            }
            else
            {
                blackBackroundForBeamer.color = new Color(blackBackroundForBeamer.color.r, blackBackroundForBeamer.color.g, blackBackroundForBeamer.color.b, 0);
                
                tabletCamera.transform.position = new Vector3(-0f, 1.58f, -10f);
                leftBeamerCamera.transform.position = new Vector3(-0f, 1.58f, -10f);
                rightBeamerCamera.transform.position = new Vector3(-0f, 1.58f, -10f);
                
                canvas.gameObject.SetActive(true);
                canvasActive = true;
                
                tabletCamera.orthographicSize = 5f;
                leftBeamerCamera.orthographicSize = 5f;
                rightBeamerCamera.orthographicSize = 5f;

                // durch das wechseln zurück in den Bearbeitungsmodus, wird der Song wieder von begin an abgespielt
                MusicManager.instance.GetComponent<AudioSource>().Play();
                canvasForBeamer.gameObject.SetActive(false);
            }
        }
    }
}