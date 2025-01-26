using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private float timeout = 0.5f;
    private float timer = 0.5f;
    private bool visible = true;
    private TextMeshProUGUI tmp;


    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            visible = !visible;
            timer = timeout;
        }
        if (tmp)
        {
            tmp.enabled = visible;
        }   
    }
}
