using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = System.Object;

public class AnimationPanelManager : MonoBehaviour
{
    public GameObject currentObject;
    private AnimationHandler _animationHandler;

    // called when an object is clicked
    public void SetCurrentObject(GameObject obj)
    {
        if (currentObject)
        {
            AnimationHandler oldHandler = currentObject.GetComponent<AnimationHandler>();
            if (oldHandler)
            {
                oldHandler.SetHighlight(false, "AnimationPanelManager.SetCurrentObject()");
            }
        }

        AnimationHandler newHandler = obj.GetComponent<AnimationHandler>();
        if (newHandler)
        {
            newHandler.SetHighlight(true, "AnimationPanelManager.SetCurrentObject()");
        }

        currentObject = obj;
        _animationHandler = currentObject.GetComponent<AnimationHandler>();
    }

    public void Reset()
    {
        if (currentObject)
        {
            AnimationHandler oldHandler = currentObject.GetComponent<AnimationHandler>();
            oldHandler.SetHighlight(false, "AnimationPanelManager.Reset()");
        }

        currentObject = null;
        _animationHandler = null;
    }

    public void ResetIfNotMoving()
    {
        if (_animationHandler == null)
        {
            Reset();
            return;
        }

        ObjectAniimationPanel panel = _animationHandler.GetHighlightObject().GetComponent<ObjectAniimationPanel>();
        if (!panel)
        {
           Reset(); 
           return;
        }
        
        if (panel.movePrimed)
        {
            return;
        }
        
        Reset();
    }
}