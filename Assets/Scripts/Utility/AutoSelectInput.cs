using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;

public class AutoSelectInput : UIBehaviour
{
    private TMP_InputField _field;

    private void Awake()
    { 
        Debug.Log("Object " + name);
        base.Awake();
        _field = GetComponent<TMP_InputField>();
        Debug.Log("ipu field " + _field.name);
    }


    protected void Update()
    {
        //base.OnEnable();
        _field.Select();
        Debug.Log(_field.isFocused);
    }
}
