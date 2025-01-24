using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var node = GetComponent<Node>();
            Debug.Log("Clicked on " + node.hex.ToString());
            foreach (var hex in node.hex.Neighbours())
            {
                var grid = GameObject.FindObjectOfType<SuperGrid>();
                grid.RemoveNode(hex.Hash());
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            var node = GetComponent<Node>();
            foreach (var hex in node.hex.Neighbours())
            {
                var grid = GameObject.FindObjectOfType<SuperGrid>();
                grid.AddNode(hex);
            }
        }
    }
}
