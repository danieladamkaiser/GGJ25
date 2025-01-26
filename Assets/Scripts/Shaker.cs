using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [Header("Info")]
    private Vector3 _startPos;
    private float _timer;
    private Vector3 _randomPos;
    [SerializeField] private bool useStartPos = false;

    [Header("Settings")]
    [Range(0f, 2f)]
    public float _time = 0.2f;
    [Range(0f, 2f)]
    public float _distance = 0.1f;
    [Range(0f, 0.1f)]
    public float _delayBetweenShakes = 0f;
    
    [SerializeField] private float distanceOverride  = -1f;
    [SerializeField] private bool isPlayer = false;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnValidate()
    {
        if (_delayBetweenShakes > _time)
            _delayBetweenShakes = _time;
    }

    public void Begin()
    {
        StopAllCoroutines();
        if (!useStartPos) _startPos = transform.position;
        StartCoroutine(Shake());
    }

    public bool IsShaking()
    {
        return _timer < _time;
    }

    private IEnumerator Shake()
    {
        _timer = 0f;

        while (_timer < _time)
        {
            _timer += Time.deltaTime;
            
            float d = distanceOverride > 0 ? distanceOverride : _distance;

            if (distanceOverride > 0)
            {
                var v = new Vector3(
                    Random.Range(-d, d),
                    Random.Range(-d, d),
                    Random.Range(-d, d)
                );
                _randomPos = _startPos + v;
            }
            else
            {
                _randomPos = _startPos + (Random.insideUnitSphere * _distance);

            }

            if (isPlayer)
            {
            }
            else
            {
                transform.position = _randomPos;
            }


            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }
        if (!isPlayer) transform.position = _startPos;
    }
}
