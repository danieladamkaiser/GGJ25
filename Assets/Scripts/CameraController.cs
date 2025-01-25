using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Hex center;
    [SerializeField] private float speed = 100f;
    void Start()
    {
        center = new Hex(0, 0);
        gameObject.transform.LookAt(center.ToWorld());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(center.ToWorld(), Vector3.up, 35 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(center.ToWorld(), Vector3.up, -35 * Time.deltaTime);
        }
        else
        {
            var delta = Input.mouseScrollDelta.y * Time.deltaTime * speed;
            Vector3 dir = (center.ToWorld() - gameObject.transform.position).normalized;
            transform.position += dir * delta;
            var pos = transform.position;
            
            transform.position = pos;
            gameObject.transform.LookAt(center.ToWorld());
        }
    }

    private void OnMouseDrag()
    {
        Debug.Log("Mouse drag");
    }
}
