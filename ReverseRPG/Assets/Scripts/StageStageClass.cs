using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class StageStageClass : MonoBehaviour
{
    public StageStage StageStage;
    public List<StageStageSpellButton> SpellButtons = new List<StageStageSpellButton>();
    public EGamePad MyPad;
    public StageStageSpellButton CurrentSelected;
    public StageStageSpellButton LBSpell;
    public StageStageSpellButton RBSpell;

    private bool _block;

    public void Update()
    {
        Vector3 move = InputController.Me.GetLeftStick(MyPad);
        float x = move.x;
        float y = move.y;

        if (move.magnitude > 0.1f)
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
        if (InputController.Me.GetX(EGamePad.Pad1))
        {
            OnGoToGame();
        }
        if (InputController.Me.GetS(EGamePad.Pad1))
        {
            DisableSpell();
        }
        if (InputController.Me.GetLeftBumper(EGamePad.Pad1))
        {
            LBSpell = CurrentSelected;
        }
        if (InputController.Me.GetRightBumper(EGamePad.Pad1))
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
                CurrentSelected.Disabled = true;
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
        switch (x)
        {
            case 1:
                if (CurrentSelected.Right != null && !CurrentSelected.Right.Disabled)
                {
                    CurrentSelected = CurrentSelected.Right;
                }
                break;
            case 2:
                if (CurrentSelected.Up != null && !CurrentSelected.Up.Disabled)
                {
                    CurrentSelected = CurrentSelected.Up;
                }
                break;
            case -1:
                if (CurrentSelected.Left != null && !CurrentSelected.Left.Disabled)
                {
                    CurrentSelected = CurrentSelected.Left;
                }
                break;
            case -2:
                if (CurrentSelected.Down != null && !CurrentSelected.Down.Disabled)
                {
                    CurrentSelected = CurrentSelected.Down;
                }
                break;
        }
    }
}
