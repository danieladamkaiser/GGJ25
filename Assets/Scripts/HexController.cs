using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("0" +Input.GetMouseButton(0));
        Debug.Log("1" +Input.GetMouseButton(1));
        Debug.Log("2" +Input.GetMouseButton(2));
    }
}
