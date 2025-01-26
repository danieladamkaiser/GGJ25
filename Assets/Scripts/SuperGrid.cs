using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SuperGrid : MonoBehaviour
{
    [SerializeField]
    public int radius;
    [SerializeField]
    public GameObject prefab;

    [Range(0, 100)]
    public int percentOfRockTerrain;
    [Range(0, 100)]
    public int percentOfTreeTerrain;

    private Node currentNode;
    private Dictionary<string, Node> nodes = new Dictionary<string, Node>();
    private MarketManager market;

    public Node GetNode(Hex hex)
    {
        if (!nodes.ContainsKey(hex.Hash()))
        {
            return null;
        }
        
        return nodes[hex.Hash()];
    }

    public void SetCurrentNode(Node node)
    {
        Debug.Log("Setting current node to " + node.hex);
        if (currentNode != node)
        {
            node.ClearOverlay();
        }

        currentNode = node;
    }

    public void UnsetCurrentNode(Node node)
    {
        Debug.Log("Unsetting current node " + node.hex);
        node.ClearOverlay();

        if (currentNode == node)
        {
            currentNode = null;
        }
    }

    public Node GetCurrentNode()
    {
        return currentNode;
    }

    public Hex GetCurrentHex()
    {
        return currentNode.hex;
    }

    public Node AddNode(Hex hex)
    {
        if (nodes.ContainsKey(hex.Hash()))
        {
            return nodes[hex.Hash()];
        }
        
        var pos = hex.ToWorld();
        var obj = Instantiate(prefab, pos, Quaternion.identity);
        nodes[hex.Hash()] = obj.GetComponent<Node>();
        return nodes[hex.Hash()];
    }
    
    public void RemoveNode(Hex hex)
    {
        Debug.Log("Removing node " + hex.Hash());
        var node = GetNode(hex);
        if (node == null)
        {
            Debug.Log("Log not found");
            return;
        }
        
        Debug.Log("Removed node " + node.hex.ToString());
        nodes.Remove(hex.Hash());
        DestroyImmediate(node.gameObject);
    }
    
    void CreateGrid()
    {
        foreach (var node in nodes.Values.ToArray())
        {
            RemoveNode(node.hex);
        }

        for (int x = -radius; x < radius; x++)
        {
            for (int y = -radius; y < radius; y++)
            {
                var hex = new Hex(x, y);
                if (radius < Vector3.Distance(hex.ToWorld(), Vector3.zero))
                {
                    continue;
                }
                var node = AddNode(hex);
            }
        }
        for (int x = -radius; x < radius; x++)
        {
            for (int y = -radius; y < radius; y++)
            {
                var hex = new Hex(x, y);
                if (radius < Vector3.Distance(hex.ToWorld(), Vector3.zero))
                {
                    continue;
                }
                var node = AddNode(hex);
                if (Random.Range(0, 100) < percentOfRockTerrain)
                {
                    node.GetComponent<HexController>().ChangeHexTop(HexTopsType.Terrain, 0);
                }
                if (Random.Range(0, 100) < percentOfTreeTerrain)
                {
                    node.GetComponent<HexController>().ChangeHexTop(HexTopsType.Tree, 0);
                }
            }
        }
    }

    private void OnValidate()
    {
    }

    void Start()
    {
        market = FindObjectOfType<MarketManager>();
        CreateGrid();
    }

    void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var globalMod = market.globalModifier;
        float val = 0f;
        foreach (var node in nodes.Values)
        {
            val += node.GetComponent<HexController>().GetValue() * globalMod;
        }

        market.SetValuation((int)val);
    }
}
