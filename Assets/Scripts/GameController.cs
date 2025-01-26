using Assets.Scripts.Actions;
using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayerGO;
    [SerializeField] private HexTop[] hexTops;
    [SerializeField] private InstantArgs[] instants;
    [SerializeField] private GameObject ActionItemPrefab;
    [SerializeField] public GameObject MinusEffectPrefab;
    [SerializeField] public GameObject PlusEffectPrefab;

    private Player _player;

    public static IAction[] Actions;

    void Start()
    {
        IEnumerable<IAction> actions;

        var builds = hexTops.Select(ht => new Build(ht));
        actions = instants.Select(i => new Instant(i));
        Actions = actions.Concat(builds).ToArray();
        _player = currentPlayerGO.GetComponent<Player>();
        SetupActions();
    }

    public Player GetPlayer()
    {
        return _player;
    }

    public GameObject GetHexTopPrefab(HexTopsType type)
    {
        return hexTops.Where(ht => ht.type == type).Select(ht => ht.prefab).FirstOrDefault();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetupActions()
    {
        int i = 0;
        foreach (var action in Actions)
        {
            var go = Instantiate(ActionItemPrefab, Vector3.zero, Quaternion.identity);
            go.GetComponent<ActionItem>().SetAction(action, i++);
        }
    }

    internal GameObject GetInstantPrefab(InstantType type)
    {
        return instants.Where(i => i.type == type).Select(i => i.prefab).FirstOrDefault();
    }
}
