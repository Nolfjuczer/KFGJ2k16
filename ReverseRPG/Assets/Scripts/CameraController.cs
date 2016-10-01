using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    switch (GameController.Me.PlayersCount)
	    {
	        case EPlayersCount.One:
	            Vector3 playerPos = GameController.Me.Players[0].transform.localPosition;
                transform.localPosition = new Vector3(Mathf.Clamp(playerPos.x,-5.5f,5.5f), Mathf.Clamp(playerPos.y, -5.5f, 5.5f),transform.position.z);
	            break;
            case EPlayersCount.Two:
	            break;
            default:
	            break;
	    }
	}
}


