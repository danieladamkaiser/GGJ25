
using System.Linq;
using UnityEngine;

public class Build : IAction
{
    private HexTop hexTop;
    private bool active = false;

    public Build(HexTop hexTop)
    {
        this.hexTop = hexTop;
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
            node.SetFail(hexTop.type);
            return IAction.EActionStatus.CAN_NOT_BE_DONE;
        }

        node.SetOk(hexTop.type);
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
        controller.ChangeHexTop(hexTop.type, hexTop.cost);
        OnBuild(node);
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
        return (!market.IsGameOver && market.GetCreditworthiness() >= hexTop.cost);
    }

    public GameObject GetRepresentation()
    {
        var gameController = GameObject.FindObjectOfType<GameController>();
        var prefab = gameController.GetHexTopPrefab(hexTop.type);
        return prefab;
    }

    public int GetCost()
    {
        return hexTop.cost;
    }

    public bool IsActionActive()
    {
        return active;
    }

    void OnBuild(Node node)
    {
        var market = GameObject.FindObjectOfType<MarketManager>();
        market.AddDebt(hexTop.cost);
        switch (hexTop.type)
        {
            case HexTopsType.House:
                BuildHouseEffects(market, node);
                break;
            case HexTopsType.Tree:
                BuildTreeEffects(market, node);
                break;
            case HexTopsType.Skyscrapper:
                BuildSkyscraperEffects(market, node);
                break;
            default:
                break;
        }
        market.NextIteration();
    }

    void BuildHouseEffects(MarketManager market, Node node)
    {
    }
    
    void BuildTreeEffects(MarketManager market, Node node)
    {
        foreach (var neighbour in node.GetNeighbours())
        {
            neighbour.GetComponent<HexController>().AddEffect(new ValueMultiplayer(1.1f));
        }
    }
    
    void BuildSkyscraperEffects(MarketManager market, Node node)
    {
        foreach (var neighbour in node.GetNeighbours())
        {
            neighbour.GetComponent<HexController>().AddEffect(new ValueMultiplayer(0.8f));
        }
    }
}
