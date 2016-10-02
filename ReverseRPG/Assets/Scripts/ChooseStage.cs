using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChooseStage : MonoBehaviour
{

    public Image GamePad1;
    public Image GamePad2;

    public EClasses Player1Class;
    public EClasses Player2Class;

    [SerializeField]
    private bool _block1;
    [SerializeField]
    private bool _block2;

    private Vector3 left1 = new Vector3(-2.5f,-3f,0f);
    private Vector3 left2 = new Vector3(0f, -3f, 0f);
    private Vector3 left3 = new Vector3(2.5f, -3f, 0f);

    private Vector3 right1 = new Vector3(-2.5f, -4f, 0f);
    private Vector3 right2 = new Vector3(0f, -4f, 0f);
    private Vector3 right3 = new Vector3(2.5f, -4f, 0f);

    private bool _canGoNext;

    public IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(1f);
        _canGoNext = true;
    }

    public void OnNextClick()
    {
        if (!_canGoNext) return;
        GameController.Me.Player1Class = Player1Class;
        GameController.Me.Player2Class = Player2Class;
        GameController.Me.ChangeState(EGameState.STAGE);
    }

    public void ChangeSelection(float x, int i)
    {
        switch (GameController.Me.PlayersCount)
        {
            case EPlayersCount.One:
                if (x > 0)
                {
                    if (Player1Class == EClasses.Mage)
                    {
                        Player1Class = EClasses.None;
                        GamePad1.rectTransform.localPosition = left2;
                    }
                    else if(Player1Class == EClasses.None)
                    {
                        Player1Class = EClasses.Knight;
                        GamePad1.rectTransform.localPosition = left3;
                    }
                }
                else
                {
                    if (Player1Class == EClasses.Knight)
                    {
                        Player1Class = EClasses.None;
                        GamePad1.rectTransform.localPosition = left2;
                    }
                    else if (Player1Class == EClasses.None)
                    {
                        Player1Class = EClasses.Mage;
                        GamePad1.rectTransform.localPosition = left1;
                    }
                }
                break;
            case EPlayersCount.Two:
                if (i == 1)
                {
                    if (x > 0)
                    {
                        if (Player1Class == EClasses.Mage)
                        {
                            Player1Class = EClasses.None;
                            GamePad1.rectTransform.localPosition = left2;
                        }
                        else if (Player1Class == EClasses.None)
                        {
                            Player1Class = EClasses.Knight;
                            GamePad1.rectTransform.localPosition = left3;
                        }
                    }
                    else
                    {
                        if (Player1Class == EClasses.Knight)
                        {
                            Player1Class = EClasses.None;
                            GamePad1.rectTransform.localPosition = left2;
                        }
                        else if (Player1Class == EClasses.None)
                        {
                            Player1Class = EClasses.Mage;
                            GamePad1.rectTransform.localPosition = left1;
                        }
                    }
                }
                else
                {
                    if (x > 0.3f)
                    {
                        if (Player2Class == EClasses.Mage)
                        {
                            Player2Class = EClasses.None;
                            GamePad2.rectTransform.localPosition = right2;
                        }
                        else if (Player2Class == EClasses.None)
                        {
                            Player2Class = EClasses.Knight;
                            GamePad2.rectTransform.localPosition = right3;
                        }
                    }
                    else
                    {
                        if (Player2Class == EClasses.Knight)
                        {
                            Player2Class = EClasses.None;
                            GamePad2.rectTransform.localPosition = right2;
                        }
                        else if (Player2Class == EClasses.None)
                        {
                            Player2Class = EClasses.Mage;
                            GamePad2.rectTransform.localPosition = right1;
                        }
                    }
                }
                break;
        }
    }

    // Use this for initialization
    void OnEnable()
    {
        _canGoNext = false;
        StartCoroutine(ClickDelay());
        switch (GameController.Me.PlayersCount)
        {
            case EPlayersCount.One:
                Player1Class = EClasses.Mage;
                GamePad1.rectTransform.localPosition = left1;
                GamePad1.gameObject.SetActive(true);
                GamePad2.gameObject.SetActive(false);
                break;
            case EPlayersCount.Two:
                Player1Class = EClasses.Mage;
                GamePad1.rectTransform.localPosition = left1;
                Player1Class = EClasses.Knight;
                GamePad1.rectTransform.localPosition = right3;
                GamePad1.gameObject.SetActive(true);
                GamePad2.gameObject.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float y1 = 0f;
        float y2 = 0f;
        switch (GameController.Me.PlayersCount)
        {
            case EPlayersCount.One:
                y1 = InputController.Me.GetLeftStick(EGamePad.Pad1).x;
                if (y1 != 0 && !_block1)
                {
                    _block1 = true;
                    ChangeSelection(Mathf.Sign(y1), 1);
                }
                if (y1 == 0)
                {
                    _block1 = false;
                }
                if (InputController.Me.GetX(EGamePad.Pad1))
                {
                    if (Player1Class != EClasses.None)
                        OnNextClick();
                }
                break;
            case EPlayersCount.Two:
                y1 = InputController.Me.GetLeftStick(EGamePad.Pad1).x;
                if (y1 != 0 && !_block1)
                {
                    _block1 = true;
                    ChangeSelection(Mathf.Sign(y1), 1);
                }
                if (y1 == 0)
                {
                    _block1 = false;
                }
                y2 = InputController.Me.GetLeftStick(EGamePad.Pad2).x;
                if (y2 != 0 && !_block2)
                {
                    _block2 = true;
                    ChangeSelection(Mathf.Sign(y2), 2);
                }
                if (y2 == 0)
                {
                    _block2 = false;
                }
                if (InputController.Me.GetX(EGamePad.Pad1) || InputController.Me.GetX(EGamePad.Pad2))
                {
                    if (Player1Class != EClasses.None && Player2Class != EClasses.None)
                        OnNextClick();
                }
                break;
        }
    }
}
