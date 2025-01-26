using UnityEngine;

public class Player : MonoBehaviour
{
    public IAction currentAction = null;
    public AudioClip startActionSound;
    public AudioClip endActionSound;
    public AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetAction(IAction action)
    {
        if (currentAction != null)
        {
            currentAction.OnCancel();
        }
        if (!action.CanBeStarted())
        {
            return;
        }
        currentAction = action;
        currentAction.OnStart();
        audioSource.clip = startActionSound;
        audioSource.Play();
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
            currentAction = null;
        }

        if (Input.GetMouseButtonDown(0) && actionUpdateResult == IAction.EActionStatus.CAN_BE_DONE)
        {
            Debug.Log("Mouse Down");
            currentAction.OnApply();
            audioSource.clip = endActionSound;
            audioSource.Play();
            currentAction = null;
        }
    }
}
