using UnityEngine;
using System.Collections;

public class GridElementDungeon : AStar.GridElement
{

	// Use this for initialization
	void Start () {
	    if (_elementIndex.x == (GameController.Me.MainGrid.GridSize.x - 1)
	        || _elementIndex.x == 0 || _elementIndex.y == 0 ||
	        _elementIndex.y == (GameController.Me.MainGrid.GridSize.y - 1))
	    {
	        Walkable = false;
	        gameObject.AddComponent<BoxCollider2D>();
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
