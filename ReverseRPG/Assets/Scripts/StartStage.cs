using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartStage : MonoBehaviour
{
    public Button Single;
    public Button Multi;
    public Button Exit;

    public Button CurrentSelected;

    private bool _blocked;

    public void OnSingleClick()
    {
        GameController.Me.ChangeState(EGameState.CHOOSE);
        GameController.Me.PlayersCount = EPlayersCount.One;
    }

    public void OnMultiClick()
    {
        GameController.Me.ChangeState(EGameState.CHOOSE);
        GameController.Me.PlayersCount = EPlayersCount.Two;
    }

    public void OnExitClick()
    {
        Application.Quit();   
    }
    
    public void ChangeSelection(float x)
    {
        _blocked = true;
        if (CurrentSelected == Single)
        {
            if (x > 0)
            {
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.white;
                CurrentSelected = Exit;
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.red;
            }
            else
            {
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.white;
                CurrentSelected = Multi;
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
        else if (CurrentSelected == Multi)
        {
            if (x > 0)
            {
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.white;
                CurrentSelected = Single;
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.red;
            }
            else
            {
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.white;
                CurrentSelected = Exit;
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
        else if (CurrentSelected == Exit)
        {
            if (x > 0)
            {
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.white;
                CurrentSelected = Multi;
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.red;
            }
            else
            {
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.white;
                CurrentSelected = Single;
                CurrentSelected.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
    }

	// Use this for initialization
	void Start ()
	{
	    CurrentSelected = Single;
        CurrentSelected.gameObject.GetComponent<Image>().color = Color.red;

    }
	
	// Update is called once per frame
	void Update ()
	{
	    float y1 = InputController.Me.GetLeftStick(EGamePad.Pad1).y;
        float y2 = InputController.Me.GetLeftStick(EGamePad.Pad2).y;
	    if (y1 != 0 && !_blocked)
	    {
	        ChangeSelection(Mathf.Sign(y1));
	    }
	    else if(y2 != 0 && !_blocked)
	    {
            ChangeSelection(Mathf.Sign(y2));
        }
	    else if(y1 ==0 && y2 == 0)
	    {
	        _blocked = false;
	    }

	    if (InputController.Me.GetX(EGamePad.Pad1) || InputController.Me.GetX(EGamePad.Pad2))
	    {
            if (CurrentSelected == Single)
            {
                OnSingleClick();
            }
            else if (CurrentSelected == Multi)
            {
               OnMultiClick();
            }
            else if (CurrentSelected == Exit)
            {
                OnExitClick();
            }
        }
	}
}
