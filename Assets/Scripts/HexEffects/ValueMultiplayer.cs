using System;
using System.Collections.Generic;
using UnityEngine;

class ValueMultiplayer : IEffect
{
    private float multiplier;
    private List<HexTopsType> types;
    
    public ValueMultiplayer(float multiplier, List<HexTopsType> types)
    {
        this.multiplier = multiplier;
        this.types = types;
    }

    bool CanBeApplied(HexTopsType type)
    {
        if (types == null)
        {
            return true;
        }
        
        return types.Exists(t => t != type);
    }

    public int GetValuation(int v)
    {
        return (int)(multiplier * v);
    }

    public GameObject GetEffectEffect()
    {
        var gc = GameObject.FindObjectOfType<GameController>();
        var prefab =  multiplier -1f > 0 ? gc.PlusEffectPrefab : gc.MinusEffectPrefab;
        var txt = prefab.GetComponent<TextUpAndDissappear>();
        if (txt != null)
        {
            txt.SetText("* " + multiplier.ToString("F2"));
            txt.SetColor(multiplier -1f > 0 ? Color.green : Color.red);
        }
        return prefab;
    }

    public Vector3 PositionModifier()
    {
        var abs = Mathf.Abs(multiplier);
        var val = abs - 1f;
        return new Vector3(0, val * 1, 0);
    }
}
