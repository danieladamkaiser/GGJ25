using System;
using UnityEngine;

class ValueMultiplayer : IEffect
{
    private float multiplier;

    public ValueMultiplayer(float multiplier)
    {
        this.multiplier = multiplier;
    }
    
    public int GetValuation(int cost)
    {
        return (int)(multiplier * cost);
    }

    public GameObject GetEffectEffect()
    {
        var gc = GameObject.FindObjectOfType<GameController>();
        return multiplier > 0 ? gc.PlusEffectPrefab : gc.MinusEffectPrefab;
    }

    public Vector3 PositionModifier()
    {
        var abs = Mathf.Abs(multiplier);
        var val = abs - 1f;
        return new Vector3(0, val * 1, 0);
    }
}
