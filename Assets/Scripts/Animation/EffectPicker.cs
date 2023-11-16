using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class EffectPicker : MonoBehaviour
{
    [SerializeField] public EffectMode effect;
    [SerializeField] public List<VisualEffectAsset> availableEffects;

    public int PickEffectMode()
    {

        effect = EffectMode.Raindrops;
        // Get the selected index from the dropdown
        int selectedEffect = 0;
        if (effect == EffectMode.Frozen)
        {
           selectedEffect = 1;
        }else if (effect == EffectMode.FWP)
        {
            selectedEffect = 3;
        }
        else if(effect == EffectMode.Electricity)
        {
            selectedEffect = 4;
        }
        return selectedEffect;
    }
}

public enum EffectMode
{
    Raindrops,
    Frozen,
    NotReady,
    FWP,
    Electricity,
    Nothing
}