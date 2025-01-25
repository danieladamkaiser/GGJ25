using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpAndDissappear : MonoBehaviour
{
    TMP_Text t;
    public float animTime = 0.5f;
    public float deltaTIme = 0f;
    private Vector3 originalPosition;
    void Start()
    {
        originalPosition = transform.position;
    }

    public void SetText(string tt)
    {
        GetComponent<TMP_Text>().SetText(tt);
    }

    public void SetColor(Color color)
    {
        GetComponent<TMP_Text>().color = color;
    }

    void Update()
    {
        deltaTIme += Time.deltaTime;
        if (deltaTIme >= animTime)
        {
            Destroy(gameObject);
            return;
        }
        
        transform.position = Vector3.Lerp(transform.position, originalPosition + Vector3.up, deltaTIme / (animTime * 0.5f));
    }
}
