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
        var node = GetComponent<Node>();
        Debug.Log("Clicked on " + node.hex.ToString());
        foreach (var hex in node.hex.Neighbours())
        {
            var grid = GameObject.FindObjectOfType<SuperGrid>();
            grid.RemoveNode(hex.Hash());
        }
    }
}
