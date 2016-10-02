using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;

public class StageStageClass : MonoBehaviour
{
    public StageStage StageStage;
    public List<StageStageSpellButton> SpellButtons = new List<StageStageSpellButton>();
    public EGamePad MyPad;
    public StageStageSpellButton CurrentSelected;
    public StageStageSpellButton LBSpell;
    public StageStageSpellButton RBSpell;

    public Text STR;
    public Text STA;
    public Text INT;
    public Text EXP;

    private bool _block;

    public void OnEnable()
    {
        switch (MyPad)
        {
            case EGamePad.Pad1:
                STR.text = GameController.Me.Players[0].MyClass.Strenght.ToString();
                STA.text = GameController.Me.Players[0].MyClass.Stamina.ToString();
                INT.text = GameController.Me.Players[0].MyClass.Inteligence.ToString();
                EXP.text = GameController.Me.Players[0].MyClass.EXP.ToString();
                break;
            case EGamePad.Pad2:
                STR.text = GameController.Me.Players[1].MyClass.Strenght.ToString();
                STA.text = GameController.Me.Players[1].MyClass.Stamina.ToString();
                INT.text = GameController.Me.Players[1].MyClass.Inteligence.ToString();
                EXP.text = GameController.Me.Players[1].MyClass.EXP.ToString();
                break;
        }
    }

    public void Update()
    {
        Vector3 move = InputController.Me.GetLeftStick(MyPad);
        float x = move.x;
        float y = move.y;

        if (move.magnitude > 0.1f && !_block)
        {
            if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
            {
                if (Mathf.Sign(x) > 0f)
                {
                    ChangeSelection(1);
                }
                else
                {
                    ChangeSelection(-1);
                }
            }
            else
            {
                if (Mathf.Sign(y) > 0f)
                {
                    ChangeSelection(2);
                }
                else
                {
                    ChangeSelection(-2);
                }
            }
        }
        if (y == 0 && x == 0)
        {
            _block = false;
        }
        if (InputController.Me.GetX(MyPad))
        {
            OnGoToGame();
        }
        if (InputController.Me.GetS(MyPad))
        {
            DisableSpell();
        }
        if (InputController.Me.GetLeftBumper(MyPad))
        {
            LBSpell = CurrentSelected;
        }
        if (InputController.Me.GetRightBumper(MyPad))
        {
            RBSpell = CurrentSelected;
        }
    }

    public void DisableSpell()
    {
        int activeSpells = 0;
        foreach (StageStageSpellButton spell in SpellButtons)
        {
            if (!spell.Disabled) ++activeSpells;
        }
        int possibleSpells = 0;
        switch (MyPad)
        {
            case EGamePad.Pad1:
                possibleSpells = GameController.Me.Players[0].MyClass.PossibleSpells;
                break;
            case EGamePad.Pad2:
                possibleSpells = GameController.Me.Players[1].MyClass.PossibleSpells;
                break;
        }
        if (activeSpells > possibleSpells)
        {
            if (CurrentSelected.Down != null && CurrentSelected.Down.Disabled)
            {
                CurrentSelected.Disabled = true;
                CurrentSelected.IMG.color = Color.gray;                
            }
        }
    }

    public void OnGoToGame()
    {
        int activeSpells = 0;
        foreach (StageStageSpellButton spell in SpellButtons)
        {
            if (!spell.Disabled) ++activeSpells;
        }
        int possibleSpells = 0;
        switch (MyPad)
        {
                case EGamePad.Pad1:
                possibleSpells = GameController.Me.Players[0].MyClass.PossibleSpells;
                break;
                case EGamePad.Pad2:
                possibleSpells = GameController.Me.Players[1].MyClass.PossibleSpells;
                break;
        }
        if (LBSpell != null && RBSpell != null && possibleSpells >= activeSpells)
        {
            StageStage.OnGoToGame(1);
        }
        else
        {
            StageStage.OnGoToGame(-1);
        }
    }

    //-1 = l |-2= d | 1 = r | 2 = u|
    private void ChangeSelection(int x)
    {
        _block = true;
        switch (x)
        {
            case 1:
                if (CurrentSelected.Right != null && !CurrentSelected.Right.Disabled)
                {
                    CurrentSelected.IMG.color = Color.white;
                    CurrentSelected = CurrentSelected.Right;
                    CurrentSelected.IMG.color = Color.yellow;
                }
                break;
            case 2:
                if (CurrentSelected.Up != null && !CurrentSelected.Up.Disabled)
                {
                    CurrentSelected.IMG.color = Color.white;
                    CurrentSelected = CurrentSelected.Up;
                    CurrentSelected.IMG.color = Color.yellow;
                }
                break;
            case -1:
                if (CurrentSelected.Left != null && !CurrentSelected.Left.Disabled)
                {
                    CurrentSelected.IMG.color = Color.white;
                    CurrentSelected = CurrentSelected.Left;
                    CurrentSelected.IMG.color = Color.yellow;
                }
                break;
            case -2:
                if (CurrentSelected.Down != null && !CurrentSelected.Down.Disabled)
                {
                    CurrentSelected.IMG.color = Color.white;
                    CurrentSelected = CurrentSelected.Down;
                    CurrentSelected.IMG.color = Color.yellow;
                }
                break;
        }
    }
}
