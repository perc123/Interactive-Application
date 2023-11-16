using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string _saveFile;
    [SerializeField] private GameObject _rocketManPrefab;

    void Awake()
    {
        // Update the field once the persistent path exists.
        _saveFile = Application.persistentDataPath + "/saveData.json";
    }

    public void Save(List<GameObject> queenObjects, List<GameObject> rocketManObjects, List<string> rocketManCredits, List<string> queenCredits)
    {
        Debug.Log("saving to " + _saveFile + "...");
        Debug.Log("queenObjects " + queenObjects.Count);
        Debug.Log("rocketManObjects " + rocketManObjects.Count);

        var queenObjectList = new List<QueenAnimationObjectData>();
        var rocketManObjectList = new List<RocketManAnimationObjectData>();
        
        rocketManObjectList.Clear();

        foreach (var obj in queenObjects)
        {
            Debug.Log("Saving queen object: " + obj.name);
            AnimationHandler handler = obj.GetComponent<AnimationHandler>();
            MouseInput.EffectMode effectMode;

            Vector3[] linePositions = null;
            LineRenderer lineRenderer = null;
            string prefabName = null;

            if (obj.GetComponent<MouseInput>())
            {
                MouseInput line = obj.GetComponent<MouseInput>();
                GameObject lineObject = line.lineObject;
                lineRenderer = lineObject.GetComponent<LineRenderer>();

                prefabName = lineObject.name.Replace("(Clone)", "");

                // Get the effect mode and effect gradient from MouseInput
                //MouseInput.EffectMode effectMode = line.effect;
                //EffectPicker effectPicker = line.GetComponent<EffectPicker>();
                effectMode = line.GetComponent<MouseInput>().effect;
                Gradient effectGradient = line.effectGradient;

                linePositions = new Vector3[lineRenderer.positionCount];
                for (int i = 0; i < linePositions.Length; i++)
                {
                    linePositions[i] = lineRenderer.GetPosition(i);
                }   
                queenObjectList.Add(new QueenAnimationObjectData(
                    linePositions, // Pass the line positions to QueenAnimationObjectData
                    lineRenderer.startColor, // Pass the line color to QueenAnimationObjectData
                    lineRenderer.startWidth, // Pass the line width to QueenAnimationObjectData
                    effectMode,
                    handler.initialPosition,
                    handler.targetPosition,
                    handler.customTargetPosition,
                    handler.initialRotation,
                    handler.rotationAmount,
                    handler.rotationDirection,
                    handler.initialScale,
                    handler.scaleAmount,
                    handler.lifespan,
                    handler.GetStartTime(),
                    prefabName
                ));
            }
            else
            {
                SpawnSprite spawnSprite = obj.GetComponent<SpawnSprite>();
                var prefab = spawnSprite.objectToSpawn;
                prefabName = prefab.name.Replace("(Clone)", "");
                
                queenObjectList.Add(new QueenAnimationObjectData(
                    handler.initialPosition,
                    handler.targetPosition,
                    handler.customTargetPosition,
                    handler.initialRotation,
                    handler.rotationAmount,
                    handler.rotationDirection,
                    handler.initialScale,
                    handler.scaleAmount,
                    handler.lifespan,
                    handler.GetStartTime(),
                    prefabName
                ));
            }


           
            
        }

        foreach (var obj in rocketManObjects)
        {
            Debug.Log("saving rocket object..." + obj.name);
           
            var prefab = _rocketManPrefab;
            
            // remove (clone) from name
            var prefabName = prefab.name;

            var animationHandler = obj.GetComponent<AnimationHandler>();
            var rocketManObjectController = obj.GetComponent<RocketManObjectController>();
            rocketManObjectList.Add(new RocketManAnimationObjectData(
                animationHandler.initialScale,
                animationHandler.lifespan,
                animationHandler.GetStartTime(),
                animationHandler.initialPosition,
                animationHandler.targetPosition,
                rocketManObjectController.GetAnswer(),
                rocketManObjectController.GetAvatarName(),
                rocketManObjectController.GetName()
            ));
        }

        // save to file
        var saveData = new SaveData(queenObjectList, rocketManObjectList, rocketManCredits, queenCredits);
        Debug.Log("queenObjectsList.Count = " + queenObjectList.Count);
        Debug.Log("rocketManObjectsList.Count = " + rocketManObjectList.Count);
        var json = JsonUtility.ToJson(saveData);
        File.WriteAllText(_saveFile, json);
    }

    public SaveData Load()
    {
        var queenObjectList = new List<QueenAnimationObjectData>();
        var rocketManObjectList = new List<RocketManAnimationObjectData>();
        var rocketManCredits = new List<string>();
        var queenCredits = new List<string>();

        // load from file if possible
        if (File.Exists(_saveFile))
        {
            var json = File.ReadAllText(_saveFile);
            var saveData = JsonUtility.FromJson<SaveData>(json);
            queenObjectList = saveData.queenObjects;
            rocketManObjectList = saveData.rocketManObjects;
            rocketManCredits = saveData.rocketManCredits;
            queenCredits = saveData.queenCredits;
        }
        else
        {
            Debug.Log("Save file not found in " + _saveFile);
        }
        
        return new SaveData(queenObjectList, rocketManObjectList, rocketManCredits, queenCredits);
    }
}