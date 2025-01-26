using Assets.Scripts.Actions;
using System;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class InstantArgs
    {
        public InstantType type;
        public int cost;
        public GameObject prefab;
    }
}
