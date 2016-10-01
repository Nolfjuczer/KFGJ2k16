using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

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

    public void CheckPlayersAlive()
    {
        bool alive = false;
        foreach (PlayerController player in Players)
        {
            if (player.MyClass.HP > 0) alive = true;
        }
        if (!alive)
        {
            Debug.LogError("All Players Died!!!");
            //todo handle game over
        }
    }

    public void OnStageOver()
    {
        Debug.LogError("Stage is Over!!!");
        //todo handle stage win
    }
}

public enum EPlayersCount
{
    One,
    Two
}
