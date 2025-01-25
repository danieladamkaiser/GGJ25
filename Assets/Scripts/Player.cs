using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public IAction currentAction;

    void Start()
    {
        currentAction = new Build(HexTopsType.House);
        currentAction.OnStart();
    }

    void Update()
    {
        if (currentAction != null)
        {
            ActionUpdate();
        }
    }

    void ActionUpdate()
    {
        var actionUpdateResult = currentAction.Update();
        if (actionUpdateResult == IAction.EActionStatus.CANCELED)
        {
            currentAction.OnCancel();
            currentAction = new Build(HexTopsType.Tree);
        }

        if (Input.GetMouseButtonDown(0) && actionUpdateResult == IAction.EActionStatus.CAN_BE_DONE)
        {
            Debug.Log("Mouse Down");
            currentAction.OnApply();
            currentAction = new Build(HexTopsType.Tree);
        }
    }
}
