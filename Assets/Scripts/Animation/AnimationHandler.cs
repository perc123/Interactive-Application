using UnityEngine;


public class AnimationHandler : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    public bool customTargetPosition = false;
    
    public Vector3 initialRotation;
    public float rotationAmount;
    public float rotationDirection = 1;
    
    public Vector3 initialScale;
    public float scaleAmount;
    
    public float lifespan = 5f;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float startTime = 0;
    
    [SerializeField] private GameObject rotationObject;
    [SerializeField] private GameObject highlightObject;
    [SerializeField] public GameObject animationObject;
    [SerializeField] private GameObject animationSprite;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D collider2D;
    
    public bool isFromCurrentSession = false;
    
    public GameObject GetHighlightObject()
    {
        return highlightObject;
    }
    
    public void SetStartTime(float startTime)
    {
        this.startTime = startTime;
    }
    
    public float GetStartTime()
    {
        return startTime;
    }

    public void SetTimer(float timer)
    {
        this.timer = timer;
    }
    
    public float GetTimer()
    {
        return timer;
    }

    // Start is called before the first frame update
    // setting initial values and calculating difference
    public void Start()
    {
        if (targetPosition == Vector3.zero)
        {
            targetPosition = transform.position;   
        }
        initialPosition = transform.position;
        initialRotation = transform.localEulerAngles;
        timer = startTime;
        //Debug.Log("localScale: " + animationObject.transform.localScale);
        initialScale = animationObject.transform.localScale;
        Debug.Log("Initial scale: " + initialScale + " was set");
    }


    // checking if the current time is within the lifespan of the animation
    // if it is, then it will play the animation
    // isplayed and timer are set by the animation manager in the video player
    void Update()
    {
        if (timer >= startTime && timer <= startTime + lifespan)
        {
            animationSprite.SetActive(true);

            animationObject.transform.position= Vector3.Lerp(initialPosition, targetPosition, (timer - startTime) / lifespan);
            
            if(rotationAmount >= 0)
            {
                // If rotationAmountTime is positive, rotate around z-axis
                rotationObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Abs(rotationAmount) * 90 * rotationDirection * (timer - startTime) / lifespan);
            }
            else
            {
                // If rotationAmountTime is negative, rotate around y-axis
                rotationObject.transform.rotation = Quaternion.Euler(0, Mathf.Abs(rotationAmount) * 90 * rotationDirection * (timer - startTime) / lifespan, 0);
            }

            //scale
            Vector3 scale = new Vector3(scaleAmount/5, scaleAmount/5, scaleAmount/5);
            animationObject.transform.localScale = initialScale + scale * (timer - startTime) / lifespan;;
        }
        else
        {
            animationSprite.SetActive(false);
        }

        if (customTargetPosition == false)
        {
            targetPosition = initialPosition;
        }

        if(spriteRenderer == null || collider2D == null)
        {
            return;
        }
        
        if (isFromCurrentSession == false)
        {
           spriteRenderer.color = new Color(1, 1, 1, 0.3f);
           collider2D.enabled = false;
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            collider2D.enabled = true;
        }

    }

    public void Reset()
    {
        animationObject.transform.position = initialPosition;
        animationObject.transform.localEulerAngles = initialRotation;
        animationObject.transform.localScale = initialScale;
        SetHighlight(false, "AnimationHandler.Reset");
    }
    
    public void SetHighlight(bool value, string caller)
    {
//        Debug.Log("Highlight set to: " + value + " by " + caller);
        highlightObject.SetActive(value);
    }
    
}