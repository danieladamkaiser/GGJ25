using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    private Vector3 _startScale;
    private float _timer;
    private float multiplier = 2f;
    

    public float _time = 0.2f;

    private void Awake()
    {
        _startScale = transform.localScale;
    }

    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        _timer = 0f;

        while (_timer < _time)
        {
            _timer += Time.deltaTime;

            if (_timer / 2f < _time / 2f)
            {
                float x = EaseOutSine(_startScale.x, _startScale.x * multiplier, (_timer / 2f) / (_time / 2f));
                float y = EaseOutSine(_startScale.y, _startScale.y * multiplier, (_timer / 2f) / (_time / 2f));
                float z = EaseOutSine(_startScale.z, _startScale.z * multiplier, (_timer / 2f) / (_time / 2f));
                
                transform.localScale = new Vector3(x, y, z);
            }
            else
            {
                float maxT = (_time / 2f);
                float t = maxT - (_timer - maxT);
                float x = EaseOutSine(_startScale.x * multiplier, _startScale.x, t / maxT);
                float y = EaseOutSine(_startScale.y * multiplier, _startScale.y, t / maxT);
                float z = EaseOutSine(_startScale.z * multiplier, _startScale.z, t / maxT);
                
                transform.localScale = new Vector3(x, y, z);
            }
            yield return null;
            
        }
        transform.localScale = _startScale;
    }
    
    public static float EaseOutSine(float start, float end, float progress)
    {
        return Mathf.Lerp(start, end, (float)Mathf.Sin(progress * Mathf.PI) / 2);
    }
}
