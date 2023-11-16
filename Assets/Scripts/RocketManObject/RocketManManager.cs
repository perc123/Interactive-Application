using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

public class RocketManManager : MonoBehaviour
{

    [SerializeField] public GameObject rocketManPrefab;


    public GameObject[] addRocketManObject(int numberOfObjects, Utility.RocketManObject rocketManObject)
    {
        GameObject[] newObjects = new GameObject[rocketManObject.Answers.Count];
        
        for (int i = 0; i < rocketManObject.Answers.Count; i++)
        {
            var prefab = Resources.Load<GameObject>(rocketManPrefab.name);   
            
            var spawnPos = new Vector3(1,1,1);
            
            var currentObject = Instantiate(prefab, spawnPos, Quaternion.identity);
            
            RocketManObjectController controller = currentObject.GetComponent<RocketManObjectController>();
            controller.SetAnswer(rocketManObject.Answers[i]);
            controller.SetAvatar(rocketManObject.Avatar);
    
            AnimationHandler handler = currentObject.GetComponent<AnimationHandler>();
            handler.SetTimer(0);

            var parentQueen = GameObject.Find("RocketManObjects");
            
            currentObject.transform.parent = parentQueen.transform;
            handler.initialScale = currentObject.transform.localScale;
            
            newObjects[i] = currentObject;
        }

        return newObjects;
    }
}
