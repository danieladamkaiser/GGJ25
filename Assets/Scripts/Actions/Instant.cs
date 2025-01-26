using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Instant : IAction
    {
        private MarketManager market;
        private InstantArgs args;

        public Instant(InstantArgs args)
        {
            market = GameObject.FindObjectOfType<MarketManager>();
            this.args = args;
        }

        public bool CanBeStarted()
        {
            return (!market.IsGameOver && market.GetCreditworthiness() >= args.cost);
        }

        public int GetCost()
        {
            return args.cost;
        }

        public GameObject GetRepresentation()
        {
            var gameController = GameObject.FindObjectOfType<GameController>();
            var prefab = gameController.GetInstantPrefab(args.type);
            return prefab;
        }

        public bool IsActionActive()
        {
            return false;
        }

        public IAction.EActionResult OnApply()
        {
            switch (args.type)
            {
                case InstantType.SellCompany:
                    market.SellCompany();
                    break;
                case InstantType.SellShares:
                    market.SellShares();
                    break;
                case InstantType.Lobbing:
                    market.Lobby();
                    break;
            }
            market.NextIteration();
            return IAction.EActionResult.SUCCESS;
        }

        public IAction.EActionResult OnCancel()
        {
            return IAction.EActionResult.SUCCESS;
        }

        public IAction.EActionResult OnStart()
        {
            return IAction.EActionResult.SUCCESS;
        }

        public IAction.EActionStatus Update()
        {
            return IAction.EActionStatus.CAN_BE_DONE;
        }
    }
}
