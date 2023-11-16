using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;

public class AnimationManager : MonoBehaviour
{
   public float time = 0;
    public float songLength = 0;

    [SerializeField] private List<GameObject> rocketManObjectList = new List<GameObject>();
    [SerializeField] private List<AnimationHandler> rocketManHandlerList = new List<AnimationHandler>();

    [SerializeField] private List<GameObject> queenObjectList = new List<GameObject>();
    [SerializeField] private List<AnimationHandler> queenHandlerList = new List<AnimationHandler>();

    public TextMeshProUGUI timerText;

    [SerializeField] private bool _isPlaying = false;

    public Camera mainCamera;

    public AnimationPanelManager animationPanelManager;
    private AnimationHandler _animationHandler;

    public Song currentSong;

    [SerializeField] private GameObject queenObjects;
    [SerializeField] private GameObject rocketManObjects;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private GameObject canvas;
    [SerializeField] private LyricsDisplayer lyricsDisplayer;
    public bool LineDrawing { get; set; }

    [SerializeField] private SaveSystem saveSystem;

    [SerializeField] private RocketManManager rocketManManager;

    [SerializeField] private GameObject mainMenu;

    public List<float> _startTimes_queenSong;
    public List<float> _startTimes_rocketmanSong;

    [SerializeField] GameObject QueenCredits;
    //public TMP_InputField RocketmannameInputField;


    public List<string> creditListRocketman = new List<string>();
    public List<string> creditListDontStopMeNow = new List<string>();
    
    [SerializeField] ControlButtonHandler controlButtonHandler;

    public List<string> GetCreditListDontStopMeNow()
    {
        return creditListDontStopMeNow;
    }

    public List<string> GetCreditListRocketman()
    {
        return creditListRocketman;
    }

    // Start is called before the first frame update
    // The animation handlers are added, these are later used to dispatch the animation events and time
    void Start()
    {
        // load from file if possible
        LoadLists();

        time = 0;

        mainCamera = Camera.main;
    }

    public void OnFinishButtonPressed()
    {
        if (name.Equals("...")) // Überprüfung, ob etwas eingegeben wurde
        {
            QueenCredits.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            creditListRocketman.Add(name);
            creditListDontStopMeNow.Add(name);
            QueenCredits.SetActive(false);
            mainMenu.SetActive(true);
        }
    }


    // called bby ui button
    public void StartTimer()
    {
        _isPlaying = true;
        animationPanelManager.Reset();
    }

    // called by ui button
    public void StopTimer()
    {
        time = 0;
        _isPlaying = false;
        SetTimerText();
        SetTimeLinePosition();
        foreach (var handler in queenHandlerList)
        {
            handler.Reset();
        }

        foreach (var handler in rocketManHandlerList)
        {
            handler.Reset();
        }
    }

    public void PauseButtonClick()
    {
        _isPlaying = false;
        SetTimerText();
        SetTimeLinePosition();
    }

    // Update is called once per frame
    // if the animation is playing, the time is increased and the animation handlers are updated
    void Update()
    {
        if (mainMenu.activeInHierarchy)
        {
            queenObjects.SetActive(true);
            rocketManObjects.SetActive(true);
        }
        else
        {
            switch (currentSong)
            {
                case Song.Queen:
                    queenObjects.SetActive(true);
                    rocketManObjects.SetActive(false);
                    break;
                case Song.RocketMan:
                    queenObjects.SetActive(false);
                    rocketManObjects.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        lyricsDisplayer.time = time;
        SetTimerText();

        if (_isPlaying)
        {
            time += Time.deltaTime;
        }

        SetTimeLinePosition();
        foreach (AnimationHandler handler in queenHandlerList)
        {
            handler.SetTimer(time);
        }

        foreach (AnimationHandler handler in rocketManHandlerList)
        {
            handler.SetTimer(time);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateTimer();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            SelectObject(mousePosition);
        }

        // Ermoeglicht es die vom134 Benutzer hinzugefuegten Elemente mit der Tastenkombination Strg + V anzuzeigen
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                activateAllGameobjekts();
            }
        }
        
        SetObjectVisibility();
    }


    public void UpdateTimer()
    {
        if (_isPlaying)
        {
            StopTimer();
            queenObjects.SetActive(false);
            rocketManObjects.SetActive(false);
        }
        else
        {
            StartTimer();
            if (queenObjects.activeInHierarchy && !canvas.activeInHierarchy)
            {
                queenObjects.SetActive(true);
            }
            else if (rocketManObjects.activeInHierarchy && !canvas.activeInHierarchy)
            {
                rocketManObjects.SetActive(true);
            }
        }
    }

    // function that is used by the newly instantiated objects to register themselves to the animation manager
    public void AddObject(GameObject obj, Song song)
    {
        AnimationHandler currentHandler = obj.GetComponent<AnimationHandler>();

        if (!currentHandler)
        {
            return;
        }

        if (song == Song.Queen)
        {
            queenObjectList.Add(obj);
            queenHandlerList.Add(currentHandler);
            _startTimes_queenSong.Add(currentHandler.GetStartTime());
        }
        else if (song == Song.RocketMan)
        {
            rocketManObjectList.Add(obj);
            rocketManHandlerList.Add(currentHandler);
        }
    }
    
    public void DeleteLastQueenObject()
    {
        if (queenObjectList.Count > 0)
        {
            GameObject lastObject = queenObjectList[queenObjectList.Count - 1];
            queenObjectList.RemoveAt(queenObjectList.Count - 1);
            AnimationHandler lastHandler = lastObject.GetComponent<AnimationHandler>();
            queenHandlerList.Remove(lastHandler);
            _startTimes_queenSong.Remove(lastHandler.GetStartTime());
            Destroy(lastHandler);
            Destroy(lastObject);
        }
    }
    
    

    //removing an instance of an object
    public void RemoveObj()
    {
        GameObject currentObject = animationPanelManager.currentObject;

        if (!currentObject)
        {
            return;
        }

        AnimationHandler currentHandler = currentObject.GetComponent<AnimationHandler>();

        if (!currentHandler)
        {
            return;
        }

        if (currentSong == Song.Queen)
        {
            queenObjectList.Remove(currentObject);
            queenHandlerList.Remove(currentHandler);
            _startTimes_queenSong.Remove(currentHandler.GetStartTime());
        }
        else if (currentSong == Song.RocketMan)
        {
            rocketManObjectList.Remove(currentObject);
            rocketManHandlerList.Remove(currentHandler);
            _startTimes_rocketmanSong.Remove(currentHandler.GetStartTime());
        }

        Destroy(currentHandler);
        Destroy(currentObject);
    }

    
    public void SetObjectVisibility()
    {
        if (currentSong == Song.Queen)
        {
            foreach (var handler in queenHandlerList)
            {
                if (handler.GetStartTime() + handler.lifespan < time || handler.GetStartTime() > time)
                {
                    handler.SetHighlight(false, "animationManager.SetObjectVisibility");
                    handler.gameObject.SetActive(false);
                }
                else
                {
                    handler.gameObject.SetActive(true);
                }
            }
        }
    }
    
    // called bz input system to select an object
    public void SelectObject(Vector2 mousePosition)
    {
        Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        if (IsPointerOverUIObject(mousePosition))
        {
            // Click is on a UI element, don't select any object.
            return;
        }
        
        // Deselect current object if there is one
        animationPanelManager.ResetIfNotMoving();
        
        int intLayer = LayerMask.NameToLayer("Default");
        int layerMask = 1 << intLayer;
        Collider2D targetObject = Physics2D.OverlapPoint(mousePositionInWorld, layerMask);
        if (targetObject && !LineDrawing)
        {
            var selectedObject = targetObject.transform.gameObject;
            AnimationHandler handler = selectedObject.GetComponent<AnimationHandler>();
            
            handler.Reset();
            time = handler.GetStartTime();
            SetTimeLinePosition();
            SetTimerText();
            animationPanelManager.SetCurrentObject(selectedObject);
        }
    }
    
    private bool IsPointerOverUIObject(Vector2 mousePosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(mousePosition.x, mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // Check if the hit UI element has the specific tag
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("animationPanelElement"))
            {
                return true;
            }
        }
    
        return false;
    }

    public void SetCurrentSong(int song)
    {
        currentSong = (Song)song;

        if (song == 0)
        {
            songLength = 220;
        }
        else if (song == 1)
        {
            songLength = 288;
        }

        time = 0;
        SetTimerText();
        SetTimeLinePosition();
        lyricsDisplayer.SetCurrentSong(song);
        timeSlider.maxValue = songLength;
    }

    public void UpdateTimeFromTimeline()
    {
        time = timeSlider.value;

        foreach (var handler in queenHandlerList)
        {
            handler.SetTimer(time);
        }
    }

    private void SetTimerText()
    {
        // show time in format mm:ss
        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void SetTimeLinePosition()
    {
        timeSlider.value = time;
    }

    // Diese Methode serialisiert alle hinzugefügten Assets und Deaktiviert Sie, damit sie nicht mehr für andere User sichtbar sind
    public void SaveLists()
    {
        foreach (var handler in queenHandlerList)
        {
            handler.isFromCurrentSession = false;
        }
        saveSystem.Save(queenObjectList, rocketManObjectList, creditListRocketman, creditListDontStopMeNow);
        foreach (Transform child in rocketManObjects.transform)
        {
            GameObject childGameObject = child.gameObject;
            childGameObject.SetActive(false);
        }

        foreach (Transform child in queenObjects.transform)
        {
            GameObject childGameObject = child.gameObject;
            childGameObject.SetActive(false);
        }
    }


    //aktiviert alle Rocketman und Queenobjekte
    public void activateAllGameobjekts()
    {
        foreach (Transform child in rocketManObjects.transform)
        {
            GameObject childGameObject = child.gameObject;
            childGameObject.SetActive(true);
        }

        foreach (AnimationHandler handler in queenHandlerList)
        {
            handler.isFromCurrentSession = true;
        }

        foreach (AnimationHandler handler in rocketManHandlerList)
        {
            handler.isFromCurrentSession = true;
        }

        foreach (Transform child in queenObjects.transform)
        {
            GameObject childGameObject = child.gameObject;
            childGameObject.SetActive(true);
        }
    }

    //Laden von rocketManObjects und queenObjects ist nur noch möglich, wenn keine vorhanden sind. 
    // Dies ist der Fall, wenn die Anwendung neu gestartet wird
public void LoadLists()
{
    int childCountRocket = rocketManObjects.transform.childCount;
    int childCountQueen = queenObjects.transform.childCount;

    if (childCountRocket == 0 && childCountQueen == 0)
    {
        var saveData = saveSystem.Load();

        foreach (var queenObjectData in saveData.queenObjects)
        {
            // Get prefab based on name
            var prefab = Resources.Load<GameObject>(queenObjectData.prefabName);
            // Instantiate prefab
            var currentObject = Instantiate(prefab, queenObjectData.initialPosition, Quaternion.identity);

            AnimationHandler handler = currentObject.GetComponent<AnimationHandler>();
            handler.initialPosition = queenObjectData.initialPosition;
            handler.targetPosition = queenObjectData.targetPosition;
            handler.initialRotation = queenObjectData.initialRotation;
            handler.rotationAmount = queenObjectData.rotationAmount;
            handler.rotationDirection = queenObjectData.rotationDirection;
            handler.initialScale = queenObjectData.initialScale;
            handler.scaleAmount = queenObjectData.scaleAmount;
            handler.SetStartTime(queenObjectData.startTime);
            handler.lifespan = queenObjectData.lifespan;
            handler.customTargetPosition = queenObjectData.customTargetPosition;
            handler.SetTimer(0);

            if (currentObject.GetComponent<LineRenderer>())
            {
                // Set line positions, color, and width
                LineRenderer lineRenderer = currentObject.GetComponent<LineRenderer>();
                lineRenderer.positionCount = queenObjectData.linePositions.Length;
                lineRenderer.SetPositions(queenObjectData.linePositions);
                lineRenderer.startColor = lineRenderer.endColor = queenObjectData.color;
                lineRenderer.startWidth = lineRenderer.endWidth = queenObjectData.lineWidth;
                lineRenderer.GetComponent<VisualEffect>().visualEffectAsset = 
                    GetComponent<MouseInput>().availableEffects[(int) queenObjectData.effect];
                currentObject.GetComponent<MouseInput>().effect = queenObjectData.effect;

                // Set the effect gradient color
                VisualEffect visuEffect = lineRenderer.GetComponent<VisualEffect>();
                MouseInput mouseInput = GetComponent<MouseInput>();
                visuEffect.SetGradient("Color", mouseInput.effectGradient);
            
                Mesh mesh = new() {name = "Line"};
                lineRenderer.BakeMesh(mesh);
                lineRenderer.GetComponent<VisualEffect>().SetMesh("LineMesh", mesh);
                lineRenderer.enabled = false; 
            }
            
            creditListRocketman = saveData.rocketManCredits;
            creditListDontStopMeNow = saveData.queenCredits;

            // Set parent
            var parentQueen = GameObject.Find("QueenObjects");
            currentObject.transform.parent = parentQueen.transform;
            // Add object to list
            AddObject(currentObject, Song.Queen);
        }

        foreach (var rocketManObject in saveData.rocketManObjects)
        {
            // Instantiate prefab
            var currentObject = Instantiate(rocketManManager.rocketManPrefab, rocketManObject.initialPosition,
                Quaternion.identity);

            AnimationHandler handler = currentObject.GetComponent<AnimationHandler>();
            RocketManObjectController rocketManObjectController =
                currentObject.GetComponent<RocketManObjectController>();
            handler.SetStartTime(rocketManObject.startTime);
            handler.lifespan = rocketManObject.lifespan;
            handler.initialPosition = rocketManObject.initialPosition;
            handler.targetPosition = rocketManObject.targetPosition;
            handler.initialScale = rocketManObject.initialScale;
            handler.SetTimer(0);

                rocketManObjectController.SetAvatarByName(rocketManObject.avatarName);
                rocketManObjectController.SetAnswer(rocketManObject.answer);
                rocketManObjectController.SetName(rocketManObject.name);

            // Set parent
            var parentQueen = GameObject.Find("RocketManObjects");
            currentObject.transform.parent = parentQueen.transform;
            // Add object to list
            AddObject(currentObject, Song.RocketMan);
        }
        StopTimer();
    }
}




    //ToDo: notwendig? Es gibt schon addObject method()
    public void addRocketManObject(Utility.RocketManObject rocketManObject)
    {
        GameObject[] newObjects = rocketManManager.addRocketManObject(rocketManObjectList.Count, rocketManObject);

        for (int i = 0; i < newObjects.Length; i++)
        {
            Debug.Log("object id" + newObjects[i].GetInstanceID());
            Debug.Log("handler id: " + newObjects[i].GetComponent<AnimationHandler>().GetInstanceID());
            AddObject(newObjects[i], Song.RocketMan);
        }
    }

    public void changeLifespanRocketmanObjects()
    {
        List<Vector3> _spawnOptions = new List<Vector3>
        {
            new Vector3(-4.5f, -2.4f, 0),
            new Vector3(4.2f, 3.4f, 0),
        };

        List<Vector3> _targetOptions = new List<Vector3>
        {
            new Vector3(-4.5f, 3.4f, 0),
            new Vector3(4.2f, -2.4f, 0),
        };

        RandomizeList(rocketManHandlerList);
        // calculations for start times and number of elements to show
        // x - amount of elements to show
        // y - amount of overlap items can have (1 = no overlap, 2 = two items show at the same time)
        // z - song length in seconds
        // a - time one element is shown if list is full (minimum time one element is shown)
        //
        // formula z = ((x - 1) * (a / y)) + a * y)
        // example: 40 = ((40 - 1) * (7.2 / 1)) + 7.2 * 1)
        // current with 58 elements (rounded): 288 = ((58 - 1) * (7.2 / 1.5)) + 7.2 * 1.5)


        // Entfernen von Elementen, falls nötig
        int count = rocketManHandlerList.Count;
        if (count > 58)
        {
            rocketManHandlerList.RemoveRange(58, rocketManHandlerList.Count - 58);
        }

        int songLengthInSeconds = 288; // Gesamtdauer des Rocketman Songs in Sekunden
        int totalNumberOfAssets = rocketManHandlerList.Count;

        float lifespanPerAsset = (float)songLengthInSeconds / totalNumberOfAssets;

        //Neusetzen der lifespan und start Time aller Elemente
        for (int i = 0; i < rocketManHandlerList.Count; i++)
        {
            AnimationHandler handler = rocketManHandlerList[i];

            handler.lifespan = lifespanPerAsset * 1.5f;
            handler.SetStartTime(i * lifespanPerAsset / 1.5f);
            handler.initialPosition = _spawnOptions[i % 2];
            handler.targetPosition = _targetOptions[i % 2];
        }
    }

    static void RandomizeList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}