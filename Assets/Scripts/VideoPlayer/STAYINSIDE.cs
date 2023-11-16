using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STAYINSIDE : MonoBehaviour
{
    public float minX = -5f;
    public float maxX = 7f;
    public float minY = -3.2f;
    public float maxY = 3.2f;
    
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
    }
}
