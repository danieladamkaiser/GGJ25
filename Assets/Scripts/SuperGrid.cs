using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuperGrid : MonoBehaviour
{
    [SerializeField]
    public int radius;
    [SerializeField]
    public GameObject prefab;

    private Dictionary<string, Node> nodes = new Dictionary<string, Node>();

    public Node GetNode(string hash)
    {
        if (!nodes.ContainsKey(hash))
        {
            return null;
        }
        
        return nodes[hash];
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
    
    public void RemoveNode(string hash)
    {
        Debug.Log("Removing node " + hash);
        var node = GetNode(hash);
        if (node == null)
        {
            Debug.Log("Log not found");
            return;
        }
        
        Debug.Log("Removed node " + node.hex.ToString());
        nodes.Remove(hash);
        DestroyImmediate(node.gameObject);
    }
    
    void CreateGrid()
    {
        foreach (var node in nodes.Values.ToArray())
        {
            RemoveNode(node.hex.Hash());
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
                AddNode(hex);
            }
        }
    }

    private void OnValidate()
    {
    }

    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
