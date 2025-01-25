
using System.Linq;
using UnityEngine;

public class Build : IAction
{
    private HexTopsType type;
    private int cost = 0;
    private bool active = false;

    public Build(HexTopsType _type)
    {
        type = _type;
        switch (type)
        {
            case HexTopsType.House:
                cost = 1000;
                break;
            case HexTopsType.Tree:
                cost = 500;
                break;
            case HexTopsType.Skyscrapper:
                cost = 5000;
                break;
            default:
                cost = 0;
                break;
        }
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
        active = true;
        return IAction.EActionResult.SUCCESS;
    }

    public IAction.EActionResult OnApply()
    {
        active = false;
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
        OnBuild();
        return IAction.EActionResult.SUCCESS;
    }

    public IAction.EActionResult OnCancel()
    {
        active = false;
        return IAction.EActionResult.SUCCESS;
    }

    public bool CanBeStarted()
    {
        var market = GameObject.FindObjectOfType<MarketManager>();
        if (market.GetCreditworthiness() >= cost)
        {
            return true;
        }
        return false;
    }

    public GameObject GetRepresentation()
    {
        var gameController = GameObject.FindObjectOfType<GameController>();
        var prefab = gameController.GetHexTopPrefab(type);
        return prefab;
    }

    public int GetCost()
    {
        return cost;
    }

    public bool IsActionActive()
    {
        return active;
    }

    void OnBuild()
    {
        var market = GameObject.FindObjectOfType<MarketManager>();
        market.AddDebt(cost);
        switch (type)
        {
            case HexTopsType.House:
                BuildHouseEffects(market);
                break;
            case HexTopsType.Tree:
                BuildTreeEffects(market);
                break;
            case HexTopsType.Skyscrapper:
                BuildSkyscraperEffects(market);
                break;
            default:
                break;
        }
        market.NextIteration();
    }

    void BuildHouseEffects(MarketManager market)
    {
        market.AddValuation(cost);
    }
    
    void BuildTreeEffects(MarketManager market)
    {
        market.AddValuation(cost);
    }
    
    void BuildSkyscraperEffects(MarketManager market)
    {
        market.AddValuation(cost);
    }
}
