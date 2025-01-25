using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Hex center;
    [SerializeField] private float speed = 100f;
    public float rotation;
    void Start()
    {
        center = new Hex(0, 0);
        gameObject.transform.LookAt(center.ToWorld());
    }

    public static void RotateAround(Vector3 point, Transform transform, bool left)
    {
        if (left)
        {
            transform.RotateAround(point, Vector3.up, 35 * Time.deltaTime);
        }
        else
        {
             transform.RotateAround(point, Vector3.up, -35 * Time.deltaTime);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateAround(center.ToWorld(), transform, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateAround(center.ToWorld(), transform, false);
        }
        else
        {
            var delta = Input.mouseScrollDelta.y * Time.deltaTime * speed;
            Vector3 dir = (center.ToWorld() - gameObject.transform.position).normalized;
            transform.position += dir * delta;
            var pos = transform.position;
            
            transform.position = pos;
            gameObject.transform.LookAt(center.ToWorld());
            rotation = 0;
        }
    }

    private void OnMouseDrag()
    {
        Debug.Log("Mouse drag");
    }
}
