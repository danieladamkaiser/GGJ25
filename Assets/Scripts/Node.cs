using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour {
    [Range(0, 5)]
    public int dir;
    public bool randomizeDir = false;
    public bool lockY = false;
    private HexController _controller;
    private bool hasOverlay = false;
    private bool isOk = false;

    void Start()
    {
        _controller = GetComponent<HexController>();
    }

    public Hex hex {
        get {
            return transform.position.ToHex();
        }
    }

    public Hex localHex {
        get {
            return transform.localPosition.ToHex();
        }
    }

    public void ClearOverlay()
    {
        hasOverlay = false;
        _controller.StopHexTopPreview();
    }
    
    public void SetOk(HexTopsType type = HexTopsType.None)
    {
        if (hasOverlay && isOk) return;
        isOk = true;
        hasOverlay = true;
        _controller.HexTopPreview(type, true);
    }

    public void SetFail(HexTopsType type = HexTopsType.None)
    {
        if (hasOverlay && !isOk) return;
        isOk = false;
        hasOverlay = false;
        _controller.HexTopPreview(type, false);
    }

    public void ApplyTransform() {
        if (randomizeDir) {
            Hex hex = this.hex;
            int i = hex.q * 100 + hex.r;
            dir = ((i % 6) + 6) % 6;
        }
        float y = lockY ? 0f : transform.localPosition.y;
        Vector3 newPos = this.localHex.ToWorld(y);
        transform.localPosition = newPos;
        transform.localRotation = Quaternion.Euler(0, -60f * dir, 0);
    }

    void Awake()
    {
        ApplyTransform();
    }

    public List<Node> GetNeighbours()
    {
        List<Node> neighbours = new List<Node>();
        foreach (var h in hex.Neighbours())
        {
            var grid = FindObjectOfType<SuperGrid>();
            var n = grid.GetNode(h);
            if (n != null) neighbours.Add(n);
        }
        return neighbours;
    }

    public List<Node> GetNeighborsRange(int r)
    {
        List<Node> neighbours = new List<Node>();
        foreach (var h in Hex.Spiral(hex, 0, r))
        {
            Debug.Log(h);
            var grid = FindObjectOfType<SuperGrid>();
            var n = grid.GetNode(h);
            if (n != null) neighbours.Add(n);
        }
        return neighbours;
    }

#if UNITY_EDITOR
    protected virtual void Update() {
        if (!Application.isPlaying) {
            ApplyTransform();
            // Hack to never re-apply dir to instances
            this.dir += 1;
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(this);
            this.dir = (dir - 1) % 6;
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(this);
        }
    }

    void OnDrawGizmosSelected() {
        UnityEditor.Handles.Label(transform.position, hex.ToString());
    }
#endif

}