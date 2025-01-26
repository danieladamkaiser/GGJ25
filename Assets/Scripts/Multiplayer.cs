using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Multiplayer : MonoBehaviour
{
    [SerializeField] private GameObject go;
    Shaker shaker;
    private int multipleyer;
    
    void Start()
    {
        shaker = GetComponent<Shaker>();
    }

    void Update()
    {
        if (multipleyer > 4)
        {
            shaker.Begin();
        }
        else
        {
            shaker.StopAllCoroutines();
        }

        go.SetActive(multipleyer > 1);
    }

    public void SetMultiplayer(int mp)
    {
        if (mp > multipleyer)
        {
            shaker.Begin();
        }

        if (mp != multipleyer)
        {
            go.GetComponent<TextMeshProUGUI>().text = "x" + mp.ToString();
        }
        
        multipleyer = mp;
    }
}
