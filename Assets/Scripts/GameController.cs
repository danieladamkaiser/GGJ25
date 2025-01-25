using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayerGO;
    [SerializeField] private HexTop[] hexTops;
    [SerializeField] private GameObject ActionItemPrefab;
    [SerializeField] public GameObject PlusEffectPrefab;
    [SerializeField] public GameObject MinusEffectPrefab;

    private Player _player;

    public static IAction[] Actions =
    {
        new Build(HexTopsType.House),
        new Build(HexTopsType.Tree),
        new Build(HexTopsType.Skyscrapper),
    };

    void Start()
    {
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
}
