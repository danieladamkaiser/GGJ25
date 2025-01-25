using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public partial class HexController : MonoBehaviour
{
    public HexTop[] hexTops;

    public HexTopsType currentHexTop;
    private GameObject currentHexTopGO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeHexTop(HexTopsType hexTopsType)
    {
        currentHexTop = hexTopsType;

        if (currentHexTopGO != null)
        {
            GameObject.Destroy(currentHexTopGO);
            currentHexTopGO = null;
        }

        var prefab = hexTops.Where(ht => ht.type == currentHexTop).Select(ht => ht.prefab).FirstOrDefault();

        if (prefab != null)
        {
            currentHexTopGO = Instantiate(prefab, transform);
        }

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left");
            ChangeHexTop(HexTopsType.Tree);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right");
            ChangeHexTop(HexTopsType.House);
        }
    }

    private void CreateNeighbours()
    {
        var node = GetComponent<Node>();
        Debug.Log("Clicked on " + node.hex.ToString());
        foreach (var hex in node.hex.Neighbours())
        {
            var grid = GameObject.FindObjectOfType<SuperGrid>();
            grid.AddNode(hex);
        }
    }

    private void RemoveNeighbours()
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
