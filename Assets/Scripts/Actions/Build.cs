
using UnityEngine;

public class Build : IAction
{
    private HexTopsType type;

    public Build(HexTopsType _type)
    {
        type = _type;
    }
    
    public IAction.EActionStatus Update()
    {
        var grid = GameObject.FindObjectOfType<SuperGrid>();
        var node = grid.GetCurrentNode();
       
        if (node == null)
        {
            return IAction.EActionStatus.CAN_NOT_BE_DONE;
        }

        var controller = node.GetComponent<HexController>();
        if (controller.Occupied())
        {
            node.SetFail(type);
            return IAction.EActionStatus.CAN_NOT_BE_DONE;
        }

        node.SetOk(type);
        return IAction.EActionStatus.CAN_BE_DONE;
    }

    public IAction.EActionResult OnStart()
    {
        return IAction.EActionResult.SUCCESS;
    }

    public IAction.EActionResult OnApply()
    {
        var grid = GameObject.FindObjectOfType<SuperGrid>();
        var node = grid.GetCurrentNode();
       
        if (node == null)
        {
            return IAction.EActionResult.FAILURE;
        }

        var controller = node.GetComponent<HexController>();
        
        if (node == null || controller.Occupied())
        {
            return IAction.EActionResult.FAILURE;
        }

        Debug.Log("Action ok");
        node.ClearOverlay();
        controller.ChangeHexTop(type);
        return IAction.EActionResult.SUCCESS;
    }

    public IAction.EActionResult OnCancel()
    {
        return IAction.EActionResult.SUCCESS;
    }

    public bool CanBeStarted()
    {
        return true;
    }
}
