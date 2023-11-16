using System.Collections.Generic;
using System.Linq;
using CENTIS.Homography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Homography
{
    [ExecuteInEditMode]
    public class HomographyManager : MonoBehaviour
    {
        
    [SerializeField] KeyCode handleToggleKey = KeyCode.Hash;

    [SerializeField] private GameObject draggablePointPrefab;
    [SerializeField] private HomographyRenderFeature homographyRenderFeature;
    [SerializeField] private ScriptableRendererData rendererData;

    [SerializeField] int layer = 31;

    [SerializeField] private bool debugOutput;

    [SerializeField, HideInInspector] private Camera mainCamera;
    [SerializeField, HideInInspector] private Camera uiCamera;

    [SerializeField, HideInInspector] private Shader shader;

    [SerializeField] private Material material;

    [SerializeField, HideInInspector] private DraggablePoint v00;
    [SerializeField, HideInInspector] private DraggablePoint v01;
    [SerializeField, HideInInspector] private DraggablePoint v10;
    [SerializeField, HideInInspector] private DraggablePoint v11;

    private float[] _homography;
    private float[] _invHomography;

    bool _isHandlesVisible = true;
    [SerializeField] private BeamerSide beamerSide = BeamerSide.Left;

    #region Unity Life-cycle
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();

        CreateCameraIfNeeded();
        uiCamera.cullingMask = 1 << layer;

        CreateVertexIfNeeded(ref v00, new Vector3(-5f, -5f, 10f));
        CreateVertexIfNeeded(ref v01, new Vector3(-5f,  5f, 10f));
        CreateVertexIfNeeded(ref v10, new Vector3( 5f, -5f, 10f));
        CreateVertexIfNeeded(ref v11, new Vector3( 5f,  5f, 10f));
        v00.gameObject.layer = layer;
        v01.gameObject.layer = layer;
        v10.gameObject.layer = layer;
        v11.gameObject.layer = layer;
        
        
        if (!homographyRenderFeature)
        {
            homographyRenderFeature = TryGetRenderFeature() as HomographyRenderFeature;
        }
        
        if (homographyRenderFeature is null) return;
        
        CreateMaterialIfNeeded();

        LoadVertexPositions();
        
        RecalculateHomography();

        // _homography = homographyRenderFeature.Homography;
        // _invHomography = homographyRenderFeature.InvHomography;
    }

    private void OnEnable()
    {
        if (!v00 || !v01 || !v10 || !v11) return;

        v00.HasChanged += RecalculateHomography;
        v01.HasChanged += RecalculateHomography;
        v10.HasChanged += RecalculateHomography;
        v11.HasChanged += RecalculateHomography;
        
        RenderPipelineManager.endContextRendering += OnEndCameraRendering;
    }

    private void OnDisable()
    {
        if (!v00 || !v01 || !v10 || !v11) return;
        v00.HasChanged -= RecalculateHomography;
        v01.HasChanged -= RecalculateHomography;
        v10.HasChanged -= RecalculateHomography;
        v11.HasChanged -= RecalculateHomography;
        
        RenderPipelineManager.endContextRendering -= OnEndCameraRendering;
    }

    private void Update()
    {
        if (Input.GetKeyDown(handleToggleKey))
        {
            if (_isHandlesVisible)
            {
                SaveVertexPosition();
            }
            
            _isHandlesVisible = !_isHandlesVisible;

            if(debugOutput) Debug.Log($"Handle visibility set to {_isHandlesVisible}");
            v00.gameObject.SetActive(_isHandlesVisible);
            v01.gameObject.SetActive(_isHandlesVisible);
            v10.gameObject.SetActive(_isHandlesVisible);
            v11.gameObject.SetActive(_isHandlesVisible);
        }
    }

    // Called in Built-In Renderpipeline
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_homography.Length == 0 || _invHomography.Length == 0) return;
        
        material.SetFloatArray("_Homography", _homography);
        material.SetFloatArray("_InvHomography", _invHomography);
        Graphics.Blit(source, destination, material);
    }
    
    // Called in SRP Renderpipeline
    private void OnEndCameraRendering(ScriptableRenderContext context, List<Camera> cameras)
    {
        if (_homography.Length == 0 || _invHomography.Length == 0) return;
        
        material.SetFloatArray("_Homography", _homography);
        material.SetFloatArray("_InvHomography", _invHomography);
        
        // Not really necessary, maybe for future use
        // if (GraphicsSettings.currentRenderPipeline != null)
        // {
        //     homographyRenderFeature.Homography = hmatrix;
        //     homographyRenderFeature.InvHomography = inverseMatrix;
        // }
        
        // CommandBuffer cmd = new();
        // cmd.Blit(BuiltinRenderTextureType.CameraTarget, BuiltinRenderTextureType.CameraTarget, material);
        // context.ExecuteCommandBuffer(cmd);
        // cmd.Release();
    }
    
    #endregion
    
    #region Homography Calculation
    
    private void RecalculateHomography()
    {
#if UNITY_EDITOR
        if(debugOutput) Debug.Log("Calculating Homography");
#endif
        float[] hmatrix = CalcHomographyMatrix();
        _homography = hmatrix;
        float[] inverseMatrix = CalcInverseMatrix(hmatrix);
        _invHomography = inverseMatrix;
    }
    
    float[] CalcHomographyMatrix()
    {
        Vector2 p00 = v00.ViewPosition;
        Vector2 p01 = v01.ViewPosition;
        Vector2 p10 = v10.ViewPosition;
        Vector2 p11 = v11.ViewPosition;

        float x00 = p00.x;
        float y00 = p00.y;
        float x01 = p01.x;
        float y01 = p01.y;
        float x10 = p10.x;
        float y10 = p10.y;
        float x11 = p11.x;
        float y11 = p11.y;

        float a = x10 - x11;
        float b = x01 - x11;
        float c = x00 - x01 - x10 + x11;
        float d = y10 - y11;
        float e = y01 - y11;
        float f = y00 - y01 - y10 + y11;

        float h13 = x00;
        float h23 = y00;
        float h32 = (c * d - a * f) / (b * d - a * e);
        float h31 = (c * e - b * f) / (a * e - b * d);
        float h11 = x10 - x00 + h31 * x10;
        float h12 = x01 - x00 + h32 * x01;
        float h21 = y10 - y00 + h31 * y10;
        float h22 = y01 - y00 + h32 * y01;

        return new[] { h11, h12, h13, h21, h22, h23, h31, h32, 1f };
    }

    private float[] CalcInverseMatrix(float[] mat)
    {
        float i11 = mat[0];
        float i12 = mat[1];
        float i13 = mat[2];
        float i21 = mat[3];
        float i22 = mat[4];
        float i23 = mat[5];
        float i31 = mat[6];
        float i32 = mat[7];
        float i33 = 1f;
        float a = 1f / (
            + (i11 * i22 * i33)
            + (i12 * i23 * i31)
            + (i13 * i21 * i32)
            - (i13 * i22 * i31)
            - (i12 * i21 * i33)
            - (i11 * i23 * i32)
        );

        float o11 = ( i22 * i33 - i23 * i32) / a;
        float o12 = (-i12 * i33 + i13 * i32) / a;
        float o13 = ( i12 * i23 - i13 * i22) / a;
        float o21 = (-i21 * i33 + i23 * i31) / a;
        float o22 = ( i11 * i33 - i13 * i31) / a;
        float o23 = (-i11 * i23 + i13 * i21) / a;
        float o31 = ( i21 * i32 - i22 * i31) / a;
        float o32 = (-i11 * i32 + i12 * i31) / a;
        float o33 = ( i11 * i22 - i12 * i21) / a;

        return new[] { o11, o12, o13, o21, o22, o23, o31, o32, o33 };
    }
    
    #endregion
    
    #region Helper Methods

    private void CreateCameraIfNeeded()
    {
        if (uiCamera) return;

        GameObject go = new("uHomography UI Camera")
        {
            transform =
            {
                localPosition = new Vector3(0f, 0f, -10f)
            }
        };
        go.transform.SetParent(transform);

        uiCamera = go.AddComponent<Camera>();
        uiCamera.clearFlags = CameraClearFlags.Depth;
        uiCamera.orthographic = true;
        uiCamera.orthographicSize = 5.5f;
        uiCamera.useOcclusionCulling = false;
        uiCamera.allowHDR = false;
        uiCamera.allowMSAA = false;
        uiCamera.nearClipPlane = 1f;
        uiCamera.farClipPlane = 100f;
        uiCamera.depth = 100;
        
        if (GraphicsSettings.currentRenderPipeline != null)
        {
            UniversalAdditionalCameraData uiCameraData = uiCamera.gameObject.AddComponent<UniversalAdditionalCameraData>();
            uiCameraData.renderType = CameraRenderType.Overlay;
            uiCameraData.renderPostProcessing = true;
            
            mainCamera.GetComponent<UniversalAdditionalCameraData>().cameraStack.Add(uiCamera);
        }

        Physics2DRaycaster raycaster = go.AddComponent<Physics2DRaycaster>();
        raycaster.eventMask = 1 << layer;
    }

    private void CreateVertexIfNeeded(ref DraggablePoint vertex, Vector3 pos)
    {
        if (vertex || !uiCamera) return;
        
        GameObject go = Instantiate(draggablePointPrefab, uiCamera.transform);
        go.transform.localPosition = pos;
        vertex = go.GetComponent<DraggablePoint>();
        Assert.IsNotNull(vertex, "The Vertex prefab does not have DraggablePoint component.");

        vertex.camera = uiCamera;
    }

    private void CreateMaterialIfNeeded()
    {
        if (homographyRenderFeature.Material) return;

        
        if (!shader)
        {
            shader = Shader.Find("uHomography/Homography");
        }
        
        if (!material)
        {
            material = new Material(shader);
        }

        homographyRenderFeature.Material = material;
    }
    
    private ScriptableRendererFeature TryGetRenderFeature()
    {
        
        ScriptableRendererFeature feature = rendererData.rendererFeatures.FirstOrDefault(f => f.name == "HomographyRenderFeature");

        if (feature != null) return feature;
            
        Debug.LogError("Renter Feature not found!");
        return null;
    }
    
    public void LoadVertexPositions()
    {
        if (!PlayerPrefs.HasKey("uHomographyVertexPositions" + beamerSide)) return;
        
        string jsonString = PlayerPrefs.GetString("uHomographyVertexPositions" + beamerSide);
        Vector2Wrapper wrapper = JsonUtility.FromJson<Vector2Wrapper>(jsonString);

        if (wrapper != null && wrapper.array.Length == 4)
        {
            v00.ViewPosition = wrapper.array[0];
            v01.ViewPosition = wrapper.array[1];
            v10.ViewPosition = wrapper.array[2];
            v11.ViewPosition = wrapper.array[3];
        }
        else
        {
            Debug.LogError("Failed to load vertex positions from PlayerPrefs.");
        }
    }
    
    public void SaveVertexPosition()
    {
        Vector2Wrapper wrapper = new Vector2Wrapper();
        wrapper.array = new Vector2[4];
        wrapper.array[0] = v00.ViewPosition;
        wrapper.array[1] = v01.ViewPosition;
        wrapper.array[2] = v10.ViewPosition;
        wrapper.array[3] = v11.ViewPosition;

        string jsonString = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("uHomographyVertexPositions" + beamerSide, jsonString);
    }
    
    [System.Serializable]
    public class Vector2Wrapper
    {
        public Vector2[] array;
    }
    
    #endregion

    }

    public enum BeamerSide
    {
        Left,
        Right
    }
}
