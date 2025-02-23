using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionItem : MonoBehaviour
{
    private IAction _action;
    private GameObject representation;
    private CameraController cameraController;
    private int baseAngle;
    private Vector3 baseScale;
    private GameController gameController;
    private bool hovered = false;
    public TMP_Text price;
    public TMP_Text value;

    void Start()
    {
        
    }

    public void SetAction(IAction action, int index)
    {
        _action = action;
        representation = Instantiate(action.GetRepresentation(), transform);
        var representationTransform = representation.transform;
        representationTransform.SetParent(transform);

        var radius = FindObjectOfType<SuperGrid>().radius;
        radius += 2;
        transform.position += new Vector3(radius, 0, 0);
        var center = new Hex(0, 0);
        if (radius <= 7)
        {
            baseAngle = -100 + index * 20;
        }
        else
        {
            baseAngle = -100 + index * 10;
        }

        transform.RotateAround(center.ToWorld(), Vector3.up, baseAngle);
        cameraController = FindObjectOfType<CameraController>();
        baseScale = transform.localScale;
        gameController = FindObjectOfType<GameController>();
    }
    
    void Update()
    {
        var center = new Hex(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            CameraController.RotateAround(center.ToWorld(), transform, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            CameraController.RotateAround(center.ToWorld(), transform, false);
        }

        if (_action.IsActionActive() || hovered)
        {
            var market = FindObjectOfType<MarketManager>();

            price.text = Mathf.RoundToInt(_action.GetCost() * market.globalModifier).ToString();
            value.text = Mathf.RoundToInt(_action.GetValue() * market.globalModifier).ToString();

            transform.localScale = baseScale * 1.7f;
            price.enabled = true;
            value.enabled = true;
        }
        else
        {
            transform.localScale = baseScale;
            price.enabled = false;
            value.enabled = false;
        }
    }

    void OnMouseEnter()
    {
        hovered = true;
    }

    void OnMouseExit()
    {
        hovered = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown - ActionItem");
        var player = gameController.GetPlayer();
        if (_action.CanBeStarted())
        {
            player.SetAction(_action);
        }
    }
}
