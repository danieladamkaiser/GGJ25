using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperGrid : MonoBehaviour
{
    [SerializeField]
    public int radius;
    [SerializeField]
    public GameObject prefab;

    private Dictionary<int, Node> nodes = new Dictionary<int, Node>();
    
    void Start()
    {
        for (int x = 0; x < radius; x++)
        {
            for (int y = 0; y < radius; y++)
            {
                var hex = new Hex(x, y);
                var pos = hex.ToWorld();
                var obj = Instantiate(prefab, pos, Quaternion.identity);
                nodes[hex.GetHashCode()] = obj.GetComponent<Node>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
