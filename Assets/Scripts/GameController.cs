using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayerGO;
    private Player _player;

    public static IAction[] Actions =
    {
        new Build(HexTopsType.House),
        new Build(HexTopsType.Tree),
    };

    void Start()
    {
        _player = currentPlayerGO.GetComponent<Player>();
    }

    public Player GetPlayer()
    {
        return _player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
