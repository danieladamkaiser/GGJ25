using System.Collections;
using System.Collections.Generic;
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
        Destroy(node.gameObject);
    }
    
    void Start()
    {
        for (int x = 0; x < radius; x++)
        {
            for (int y = 0; y < radius; y++)
            {
                var hex = new Hex(x, y);
                var pos = hex.ToWorld();
                var obj = Instantiate(prefab, pos, Quaternion.identity);
                nodes[hex.Hash()] = obj.GetComponent<Node>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
