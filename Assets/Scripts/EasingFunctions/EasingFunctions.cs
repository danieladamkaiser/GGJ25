using System;
using UnityEngine;

namespace Assets.Scripts.EasingFunctions
{
    public static class EF
    {
        public static Vector3 Ease(Vector3 startPos, Vector3 endPos, float progress, EasingFunctionType functionType)
        {
            switch (functionType)
            {
                case EasingFunctionType.EaseOutSine:
                    return Bleble(startPos, endPos, progress, EaseOutSine);
                default:
                    throw new Exception("unimplemented");
            }
        }
        public static float EaseOutSine(float start, float end, float progress)
        {
            return Mathf.Lerp(start, end, (float)Math.Sin(progress * Math.PI) / 2);
        }

        private static Vector3 Bleble(Vector3 startPos, Vector3 endPos, float progress, Func<float, float, float, float> func)
        {
            return new Vector3(func(startPos.x, endPos.x, progress), func(startPos.y, endPos.y, progress), func(startPos.z, endPos.z, progress));
        }
    }

    public enum EasingFunctionType
    {
        EaseOutSine
    }
}
