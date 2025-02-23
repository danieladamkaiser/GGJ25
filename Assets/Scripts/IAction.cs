using UnityEngine;

public interface IAction
{
    public enum EActionResult
    {
        FAILURE,
        SUCCESS
    }

    public enum EActionStatus
    {
        CANCELED,
        CAN_BE_DONE,
        CAN_NOT_BE_DONE
    }

    public EActionStatus Update();
    public EActionResult OnStart();
    public EActionResult OnApply();
    public EActionResult OnCancel();
    public bool CanBeStarted();
    public GameObject GetRepresentation();
    public int GetCost();
    public int GetValue();
    public bool IsActionActive();
}
