using UnityEngine;
using System.Collections;

public class StageStage : MonoBehaviour
{
    public GameObject MageLeft;
    public GameObject MageRight;
    public GameObject KnightLeft;
    public GameObject KnightRight;

    public bool _canClick = false;

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
