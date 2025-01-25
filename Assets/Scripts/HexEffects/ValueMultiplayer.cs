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
