using UnityEngine;
using System.Collections;

public class StageStage : MonoBehaviour
{
    public GameObject MageLeft;
    public GameObject MageRight;
    public GameObject KnightLeft;
    public GameObject KnightRight;

    public int ReadyPlayers;
    private bool _canClick = false;

    public void OnGoToGame(int x)
    {
        ReadyPlayers += x;
        ReadyPlayers = Mathf.Clamp(ReadyPlayers, 0, 2);
        if (GameController.Me.PlayersCount == EPlayersCount.One && ReadyPlayers == 1)
        {
            GameController.Me.ChangeState(EGameState.GAME);
        }
        else if (GameController.Me.PlayersCount == EPlayersCount.Two && ReadyPlayers == 2)
        {
            GameController.Me.ChangeState(EGameState.GAME);
        }
    }

    void OnEnable()
    {
        _canClick = false;
        //StartCoroutine(ClickDelay());
        switch (GameController.Me.PlayersCount)
        {
            case EPlayersCount.One:
                switch (GameController.Me.Player1Class)
                {
                    case EClasses.Knight:
                        MageLeft.SetActive(false);
                        KnightLeft.SetActive(true);
                        break;
                    case EClasses.Mage:
                        MageLeft.SetActive(true);
                        KnightLeft.SetActive(false);
                        break;
                    default:
                        MageLeft.SetActive(true);
                        KnightLeft.SetActive(false);
                        break;
                }
                MageRight.SetActive(false);
                KnightRight.SetActive(false);
                break;
            case EPlayersCount.Two:
                switch (GameController.Me.Player1Class)
                {
                    case EClasses.Knight:
                        MageLeft.SetActive(false);
                        KnightLeft.SetActive(true);
                        break;
                    case EClasses.Mage:
                        MageLeft.SetActive(true);
                        KnightLeft.SetActive(false);
                        break;
                    default:
                        MageLeft.SetActive(true);
                        KnightLeft.SetActive(false);
                        break;
                }
                switch (GameController.Me.Player2Class)
                {
                    case EClasses.Knight:
                        MageRight.SetActive(false);
                        KnightRight.SetActive(true);
                        break;
                    case EClasses.Mage:
                        MageRight.SetActive(true);
                        KnightRight.SetActive(false);
                        break;
                    default:
                        MageRight.SetActive(true);
                        KnightRight.SetActive(false);
                        break;
                }
                break;
        }
    }
}
