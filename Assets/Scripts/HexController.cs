using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public partial class HexController : MonoBehaviour
{
    public HexTop[] hexTops;

    public HexTopsType currentHexTop = HexTopsType.None;
    private GameObject currentHexTopGO;
    private GameObject preview;
    [SerializeField] Material transparentMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Occupied()
    {
        Debug.Log("Ocuppation = " + currentHexTop);
        return currentHexTop != HexTopsType.None;
    }

    public void ChangeHexTop(HexTopsType hexTopsType)
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

    public void HexTopPreview(HexTopsType hexTopsType, bool isOk)
    {
        StopHexTopPreview();
        var prefab = hexTops.Where(ht => ht.type == hexTopsType).Select(ht => ht.prefab).FirstOrDefault();
        if (prefab != null)
        {
            preview = Instantiate(prefab, transform);
            preview.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            SetupRenderersForPreview(preview.GetComponentsInChildren<Renderer>(), isOk);
        }
    }

    void SetupRenderersForPreview(Renderer[] renderers, bool isOk)
    {
        foreach (var renderer in renderers)
        {
            var list = new List<Material>();

            foreach (var material in renderer.materials)
            {
                list.Add(new Material(transparentMaterial));
                var green = new Color32(135, 168, 137, 200);
                var red = new Color32(255, 0, 0, 200);
                list.Last().color = isOk ? green : red;
            }
            
            renderer.SetMaterials(list);
        }
    }

    public void StopHexTopPreview()
    {
        if (preview != null)
        {
            Destroy(preview);
        }
    }

    private void OnMouseEnter()
    {
        FindObjectOfType<SuperGrid>().SetCurrentNode(GetComponent<Node>());
    }

    private void OnMouseExit()
    {
        FindObjectOfType<SuperGrid>().UnsetCurrentNode(GetComponent<Node>());
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
            var grid = FindObjectOfType<SuperGrid>();
            grid.RemoveNode(hex);
        }
    }
}
