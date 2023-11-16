using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<QueenAnimationObjectData> queenObjects;
    public List<RocketManAnimationObjectData> rocketManObjects;
    public List<string> rocketManCredits;
    public List<string> queenCredits;

    public SaveData(List<QueenAnimationObjectData> queenObjects, List<RocketManAnimationObjectData> rocketManObjects, List<string> rocketManCredits, List<string> queenCredits)
    {
        this.queenObjects = queenObjects;
        this.rocketManObjects = rocketManObjects;
        this.rocketManCredits = rocketManCredits;
        this.queenCredits = queenCredits;
    }
}

[System.Serializable]
public class QueenAnimationObjectData
{
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    public bool customTargetPosition;

    public Vector3 initialRotation;
    public float rotationAmount;
    public float rotationDirection = 1;

    public Vector3 initialScale;
    public float scaleAmount;

    public float lifespan;
    public float startTime;

    public string prefabName;
    public MouseInput.EffectMode effect;

    // Line
    //public Vector3 linePositions;
    public Color color;
    public float lineWidth;

    public Vector3[] linePositions;

    public QueenAnimationObjectData(Vector3[] linePositions, Color color, float lineWidth, MouseInput.EffectMode effect, Vector3 initialPosition, Vector3 targetPosition, bool customTargetPosition, Vector3 initialRotation, float rotationAmount, float rotationDirection, Vector3 initialScale, float scaleAmount, float lifespan, float startTime, string prefabName)
    {
        this.linePositions = linePositions;
        this.color = color;
        this.lineWidth = lineWidth;
        Debug.Log(this.effect + "this.effect SaveData");
        this.effect = effect;
        this.initialRotation = initialRotation;
        this.targetPosition = targetPosition;
        this.customTargetPosition = customTargetPosition;
        this.initialPosition = initialPosition;
        this.rotationAmount = rotationAmount;
        this.rotationDirection = rotationDirection;
        this.initialScale = initialScale;
        this.scaleAmount = scaleAmount;
        this.lifespan = lifespan;
        this.startTime = startTime;
        this.prefabName = prefabName;
    }
    
    public QueenAnimationObjectData(Vector3 initialPosition, Vector3 targetPosition, bool customTargetPosition, Vector3 initialRotation, float rotationAmount, float rotationDirection, Vector3 initialScale, float scaleAmount, float lifespan, float startTime, string prefabName)
    {
        this.initialRotation = initialRotation;
        this.targetPosition = targetPosition;
        this.customTargetPosition = customTargetPosition;
        this.initialPosition = initialPosition;
        this.rotationAmount = rotationAmount;
        this.rotationDirection = rotationDirection;
        this.initialScale = initialScale;
        this.scaleAmount = scaleAmount;
        this.lifespan = lifespan;
        this.startTime = startTime;
        this.prefabName = prefabName;
    }
}

[System.Serializable]
public class RocketManAnimationObjectData
{
   public Vector3 initialPosition;
   public Vector3 targetPosition;
   
   public Vector3 initialScale;
   
   public float lifespan;
   public float startTime;
   
   public string answer;
   public string avatarName;
   public string name;
   
   public RocketManAnimationObjectData(Vector3 initialScale, float lifespan, float startTime, Vector3 initialPosition, Vector3 targetPosition, string answer, string avatarName, string name)
   {
      this.lifespan = lifespan;
      this.startTime = startTime;
      this.initialPosition = initialPosition;
      this.targetPosition = targetPosition;
      this.answer = answer;
      this.avatarName = avatarName;
      this.name = name;
      this.initialScale = initialScale;
   }
}

[System.Serializable]
public class HomographyData
{
    public Vector2[] points;

    public HomographyData(Vector2 v00, Vector2 v01, Vector2 v10, Vector2 v11)
    {
        points = new Vector2[4];
        points[0] = v00;
        points[1] = v01;
        points[2] = v10;
        points[3] = v11;
    }
}
