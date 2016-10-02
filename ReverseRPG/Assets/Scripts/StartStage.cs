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

    [SerializeField]
    private bool _blocked;

    public void OnSingleClick()
    {
        GameController.Me.PlayersCount = EPlayersCount.One;
        GameController.Me.ChangeState(EGameState.CHOOSE);
    }

    public void OnMultiClick()
    {
        GameController.Me.PlayersCount = EPlayersCount.Two;
        GameController.Me.ChangeState(EGameState.CHOOSE);
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
	    if (y1 != 0f && !_blocked)
	    {
	        ChangeSelection(Mathf.Sign(y1));
	    }
	    else if(y2 != -0.1f && !_blocked)
	    {
            //Second pad problems
            //ChangeSelection(Mathf.Sign(y2));
        }
        //second pad problems
	    if(y1 ==0.0f/* && y2 == -0.1f*/)
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
