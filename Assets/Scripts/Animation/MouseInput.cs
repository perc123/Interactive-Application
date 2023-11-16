using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.VFX;

public class MouseInput : MonoBehaviour
{
    public enum EffectMode
    {
        Raindrops,
        Frozen,
        NotReady,
        FWP,
        Electricity,
        Nothing
    }

    [SerializeField] private InputActionProperty drawStartedAction;
    [SerializeField] private InputActionProperty drawingAction;

    [ColorUsage(true, true)] [SerializeField] public Color color;
    [SerializeField] private float lineWidth = 0.01f;

    [SerializeField] private LineRenderer linePrefab;

    [GradientUsage(true)] [SerializeField] public Gradient effectGradient;
    [SerializeField] public EffectMode effect;
    [SerializeField] public List<VisualEffectAsset> availableEffects;

    private bool _isDrawing;
    private LineRenderer _currentLine;
    private float _minimumVertexDistance = 0.05f;
    private GameObject _currentLineObject;
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 oldPos;
    AnimationHandler animHandler;
    public GameObject lineObject;

    public List<GameObject> queenObjectList;
    public Song song;

    [SerializeField] private GameObject videoPlayer;
    [SerializeField] private AnimationManager animationManager;

    private void Update()
    {
        if (!_isDrawing) return;
        Debug.Log("write?");
        Vector2 screenPosition = Input.mousePosition;
        Vector3 worldPosition = new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane);

        float distance = Vector3.Distance(worldPosition, _currentLine.GetPosition(_currentLine.positionCount - 1));

        if (distance > _minimumVertexDistance)
        {
            int positionCount = _currentLine.positionCount;
            positionCount++;
            _currentLine.positionCount = positionCount;
            _currentLine.SetPosition(positionCount - 1, Camera.main.ScreenToWorldPoint(worldPosition));

            Mesh mesh = new Mesh { name = "Line" };
            _currentLine.BakeMesh(mesh);

            _currentLine.GetComponent<VisualEffect>().SetMesh("LineMesh", mesh);
        }
    }

    private void OnMouseDown()
    {
        Vector2 mousePosition = Input.mousePosition;
        Debug.Log("Drawing Started");
        // if (mousePosition.x > 250f && mousePosition.y > 150f && mousePosition.y < 950f)

        _isDrawing = true;
        _currentLine = InstantiateLine(mousePosition);
    }

    private void OnMouseUp()
    {
        Debug.Log("Drawing Stopped");
        _isDrawing = false;
        _currentLine = null;
    }

    private LineRenderer InstantiateLine(Vector2 mousePosition)
    {
        lineObject = Instantiate(linePrefab.gameObject);
        lineObject.GameObject().GetComponent<MouseInput>().effect = this.effect;
        videoPlayer = GameObject.Find("Videoplayer");
        animationManager = videoPlayer.GetComponent<AnimationManager>();
        AnimationHandler animationHandler = lineObject.GetComponent<AnimationHandler>();
        animationHandler.SetStartTime(animationManager.time);
        animationManager.AddObject(lineObject, song);
        //vielleicht Objekt spaeter erst einfuegen weil man noch Operationen an lineObject macht?
        if (song == Song.Queen)
        {
            var parentQueen = GameObject.Find("QueenObjects");
            lineObject.transform.parent = parentQueen.transform;
        }

        if (effect != EffectMode.Nothing)
        {
            VisualEffect visuEffect = lineObject.GetComponent<VisualEffect>();
            visuEffect.visualEffectAsset = availableEffects[(int)effect];
            visuEffect.SetGradient("Color", effectGradient);
        }

        LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
        lineRenderer.endColor = lineRenderer.startColor = color;
        Vector3 v = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
        lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(v));
        lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(v));
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
        lineRenderer.enabled = false;
        Mesh mesh = new Mesh { name = "Line" };
        lineRenderer.BakeMesh(mesh);
        lineRenderer.GetComponent<VisualEffect>().SetMesh("LineMesh", mesh);
        return lineRenderer;
    }

    public void ChangeEffect(int effectIndex)
    {
        if (effectIndex >= 0 && effectIndex < availableEffects.Count)
        {
            this.effect = (EffectMode)effectIndex;
            // Update the visual effect on the current line (if drawing)
            if (_isDrawing && _currentLine != null)
            {
                VisualEffect visuEffect = _currentLine.GetComponent<VisualEffect>();
                visuEffect.visualEffectAsset = availableEffects[(int)effect];
            }
        }
    }
}
