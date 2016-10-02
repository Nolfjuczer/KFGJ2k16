using UnityEngine;
using System.Collections;

public class GridElementDungeon : AStar.GridElement
{
    public Sprite Grass;

    public Sprite Tree;
    public Sprite Tree2;
    public Sprite Rock;
    public Sprite Rock2;
    public Sprite Bush;


	// Use this for initialization
	public void Start () {
	    if (_elementIndex.x == (GameController.Me.MainGrid.GridSize.x - 1)
	        || _elementIndex.x == 0 || _elementIndex.y == 0 ||
	        _elementIndex.y == (GameController.Me.MainGrid.GridSize.y - 1))
	    {
	        this._walkable = false;
	        gameObject.AddComponent<BoxCollider2D>();
            GameObject tmp = new GameObject();
            tmp.transform.SetParent(gameObject.transform);
            tmp.transform.localPosition = Vector3.zero;
            SpriteRenderer render = tmp.AddComponent<SpriteRenderer>();
            render.sprite = Tree;
            //gameObject.GetComponent<SpriteRenderer>().sprite = Tree2;
        }
	    else
	    {
	        if (Random.Range(0, 100) > 85)
	        {
                this._walkable = false;
                gameObject.AddComponent<BoxCollider2D>();
                GameObject tmp = new GameObject();
                tmp.transform.SetParent(gameObject.transform);
	            tmp.transform.localPosition = Vector3.zero;
	            SpriteRenderer render = tmp.AddComponent<SpriteRenderer>();
	            Sprite random = Tree;
	            switch (Random.Range(0, 5))
	            {
                    case 0:
                        random = Tree;
	                    break;
                    case 1:
                        random = Tree2;
                        break;
                    case 2:
                        random = Rock;
                        break;
                    case 3:
                        random = Rock2;
                        break;
                    case 4:
                        random = Bush;
                        break;
                }
                render.sprite = random;
            }
	    }
	}
}
