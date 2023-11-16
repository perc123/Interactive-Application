using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDrag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 oldPos;
    AnimationHandler animHandler;
    


    [Header("Ground Check")] public LayerMask Check;
    bool grounded;

    private void Start()
    {
        animHandler = GetComponent<AnimationHandler>();   
    }


    private void Update()
    {
        //ground check (???pls work)

        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }


    private void OnMouseDown()
    {
        transform.position = animHandler.initialPosition;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        
        // this line is important to register the new position of the object  with the animation handler
        animHandler.initialPosition = transform.position;

        //make it snap back to original postion
    }
}