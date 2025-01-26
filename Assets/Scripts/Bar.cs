using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] GameObject bar;
    float percentage = 0f;
    private Shaker shaker;
    private float dist;

    void Start()
    {
        shaker = GetComponent<Shaker>();
        if (shaker != null)
        {
        }
        bar.transform.localScale = new Vector3(0, 1, 1);
    }

    void Awake()
    {
    }
    
    

    void Update()
    {
        if (percentage >= 2f && shaker)
        {
            if (!shaker.IsShaking())
            {
                shaker.Begin();
            }
        }
    }
    
    public void SetBar(float percent)
    {
        percentage = percent;
        bar.transform.localScale = new Vector3(percent, 1, 1);

        if (shaker != null)
        {
            shaker.Begin();
        }
    }
}
