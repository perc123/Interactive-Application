using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Activate all available displays
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
