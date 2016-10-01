using System;
using UnityEngine;
using System.Collections.Generic;

public class GameController : Singleton<GameController>
{
    public AStar.Grid MainGrid;
    public PlayerController[] Players;
    public EPlayersCount PlayersCount;

    public override void Awake()
    {
        base.Awake();
        MainGrid = FindObjectOfType<AStar.Grid>();
        Players = FindObjectsOfType<PlayerController>();
        switch (Players.Length)
        {
            case 1:
                PlayersCount = EPlayersCount.One;
                break;
            case 2:
                PlayersCount = EPlayersCount.Two;
                break;
            default:
                PlayersCount = EPlayersCount.One;
                break;
        }
    }
}

public enum EPlayersCount
{
    One,
    Two
}
