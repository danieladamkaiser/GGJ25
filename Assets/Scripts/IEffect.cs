using UnityEngine;

public interface IEffect
{
    int GetValuation(int cost);

    Vector3 PositionModifier()
    {
        return new Vector3(0, 0, 0);
    }
    GameObject GetEffectEffect();

    bool CanBeApplied(HexTopsType type)
    {
        return true;
    }
}
