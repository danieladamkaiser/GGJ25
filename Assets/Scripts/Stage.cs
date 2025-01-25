using System;
using UnityEngine;

[Serializable]
public class Stage
{
    public StageType Type;
    [Range(0,100f)]
    public float Duration;
    public Color Color;
    [Range(0,10)]
    public int InterestRates;
    [Range(-50, 50)]
    public int GrowthRate;
}