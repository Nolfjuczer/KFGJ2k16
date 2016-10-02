using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class GameController : Singleton<GameController>
{
    public GameObject MainGameObject;
    public EGameState GameState;
    public AStar.Grid MainGrid;
    public PlayerController[] Players;
    public EPlayersCount PlayersCount;

    public EClasses Player1Class;
    public EClasses Player2Class;

    public PlayerController[] PossibleCharacters = new PlayerController[4];

    private bool _firstTime;

    public override void Awake()
    {
        //MainGrid = FindObjectOfType<AStar.Grid>();
        base.Awake();
        GameState = EGameState.START;
        _firstTime = true;
    }

    public void ChangeState(EGameState state)
    {
        EGameState prevState = GameState;
        GameState = state;
        switch (GameState)
        {
            case EGameState.START:
                break;
            case EGameState.CHOOSE:
                break;
            case EGameState.GAME:
                StageController.Me.ResetStage();
                if (prevState != EGameState.CHOOSE)
                {
                    switch (PlayersCount)
                    {
                        case EPlayersCount.One:
                            Players[0].gameObject.SetActive(true);
                            break;
                        case EPlayersCount.Two:
                            Players[0].gameObject.SetActive(true);
                            Players[1].gameObject.SetActive(true);
                            break;
                    }
                }
                break;
            case EGameState.STAGE:
                if (_firstTime)
                {
                    switch (PlayersCount)
                    {
                         case EPlayersCount.One:
                            Players = new PlayerController[1];
                            switch (Player1Class)
                            {
                                case EClasses.Knight:
                                    Players[0] = PossibleCharacters[1];
                                    break;
                                case EClasses.Mage:
                                    Players[0] = PossibleCharacters[0];
                                    break;
                            }
                            break;
                        case EPlayersCount.Two:
                            Players = new PlayerController[2];
                            switch (Player1Class)
                            {
                                case EClasses.Knight:
                                    Players[0] = PossibleCharacters[1];
                                    break;
                                case EClasses.Mage:
                                    Players[0] = PossibleCharacters[0];
                                    break;
                            }
                            switch (Player2Class)
                            {
                                case EClasses.Knight:
                                    Players[1] = PossibleCharacters[3];
                                    break;
                                case EClasses.Mage:
                                    Players[1] = PossibleCharacters[2];
                                    break;
                            }
                            break;
                    }
                    _firstTime = false;
                }
                break;
        }
        UIController.Me.OnGameStateChange();
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
        ChangeState(EGameState.STAGE);
    }
}

public enum EPlayersCount
{
    One,
    Two
}

public enum EGameState
{
    START,
    CHOOSE,
    STAGE,
    GAME
}
