using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectAniimationPanel : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject movePanel;
    [SerializeField] private GameObject scalePanel;
    [SerializeField] private GameObject rotatePanel;
    
    [SerializeField] private Slider scaleSlider;
    [SerializeField] private Slider rotateTypeSlider;
    [SerializeField] private Slider rotateDirectionSlider;
    
    [SerializeField] private AnimationHandler animationHandler;
    [SerializeField] private GameObject shadowSprite;
    
    [SerializeField] private Sprite rotateInactive;
    [SerializeField] private Sprite rotateActive;
    [SerializeField] private Sprite scaleInactive;
    [SerializeField] private Sprite scaleActive;
    [SerializeField] private Sprite moveInactive;
    [SerializeField] private Sprite moveActive;
    
    [SerializeField] private Button moveButton;
    [SerializeField] private Button scaleButton;
    [SerializeField] private Button rotateButton;
    
    [SerializeField] private Canvas canvas;
    
    
    public bool movePrimed = false;
    
    
    public void OnMoveButtonClick()
    {
        movePanel.SetActive(true);
        moveButton.image.sprite = moveActive;
        scaleButton.image.sprite = scaleInactive;
        rotateButton.image.sprite = rotateInactive;
        movePrimed = true;
        shadowSprite.SetActive(true);
        scalePanel.SetActive(false);
        rotatePanel.SetActive(false);
    }
    
    public void OnScaleButtonClick()
    {
        scaleButton.image.sprite = scaleActive;
        moveButton.image.sprite = moveInactive;
        rotateButton.image.sprite = rotateInactive;
        movePanel.SetActive(false);
        scalePanel.SetActive(true);
        rotatePanel.SetActive(false);
    }
    
    public void OnRotateButtonClick()
    {
        rotateButton.image.sprite = rotateActive;
        moveButton.image.sprite = moveInactive;
        scaleButton.image.sprite = scaleInactive;
        movePanel.SetActive(false);
        scalePanel.SetActive(false);
        rotatePanel.SetActive(true);
    }
    
    public void OnScaleSliderChange()
    {
        animationHandler.scaleAmount = scaleSlider.value;
    }
    
    public void OnRotateTypeSliderChange()
    {
        animationHandler.rotationAmount = rotateTypeSlider.value;
    }
    
    public void OnRotateDirectionSliderChange()
    {
        float rotationDirection = rotateDirectionSlider.value == 0 ? 1 : -1;
        animationHandler.rotationDirection = rotationDirection;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        canvas.worldCamera = mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (movePrimed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                animationHandler.customTargetPosition = true;
                shadowSprite.transform.position = mousePosition;
                animationHandler.targetPosition = mousePosition;
                movePrimed = false;
                movePanel.SetActive(false);
            }

        }

        if (animationHandler.customTargetPosition)
        {
            shadowSprite.SetActive(true);
        }
        shadowSprite.transform.position = animationHandler.targetPosition;
    }
}
