using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

public class Enemy : MonoBehaviour
{
    public float SpeedMultiplier;
    public CircleCollider2D AttackTrigger;
    public EEnemyState MyState;

    private AStar.AStarAgent _myAgent;
    private AStar.GridElement _currentElement;
    private Vector3 _movementDirection;
    private GameObject _target;
    private float _attackTimer;
    private float _attackDelay;
	// Use this for initialization
	void Start ()
	{
        _myAgent = GetComponent<AStar.AStarAgent>();
        MyState = EEnemyState.Wander;
        Wander();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    GoToPosition();
	    _attackTimer -= Time.deltaTime;
	    _attackTimer = Mathf.Clamp(_attackTimer, 0.0f, float.MaxValue);
    }

    private void Wander()
    {
        if(MyState == EEnemyState.Wander)
        {
            AStar.GridElement destination = _myAgent.MyGrid.Elements[Random.Range(1, _myAgent.MyGrid.GridSize.x),
                                                                     Random.Range(1, _myAgent.MyGrid.GridSize.x), 0];
            while (!destination.Walkable)
            {
                destination = _myAgent.MyGrid.Elements[Random.Range(1, _myAgent.MyGrid.GridSize.x),
                                                       Random.Range(1, _myAgent.MyGrid.GridSize.x), 0];            
            }
            _myAgent.TargetObject = destination.transform;            
        }
        else
        {
            _myAgent.TargetObject = _target.transform;
        }
        _myAgent.CalculatePath();
        GetNextElement();
    }

    private void GoToPosition()
    {
        switch (MyState)
        {
            case EEnemyState.Wander:
                if (_currentElement != null)
                {
                    transform.localPosition += _movementDirection * Time.deltaTime * SpeedMultiplier;
                    if (Vector3.Distance(transform.localPosition, _currentElement.transform.localPosition) < 0.1f)
                    {
                        GetNextElement();
                    }
                }
                else
                {
                    Wander();
                }
                break;
            case EEnemyState.Fight:
                if (Vector3.Distance(_target.transform.localPosition, gameObject.transform.localPosition) < 1f)
                    Attack();
                else
                {
                    if (_currentElement != null)
                    {
                        transform.localPosition += _movementDirection * Time.deltaTime * SpeedMultiplier;
                        if (Vector3.Distance(transform.localPosition, _currentElement.transform.localPosition) < 0.1f)
                        {
                            GetNextElement();
                        }
                    }
                    else
                    {
                        Wander();
                    }
                }
                break;
        }
    }

    private void GetNextElement()
    {
        if (_myAgent.Path.Count > 0)
        {
            _currentElement = _myAgent.Path.First();
            _myAgent.Path.Remove(_currentElement);
            GetMovementDirection();
        }
        else
        {
            _currentElement = null;
        }
    }

    private void Attack()
    {
        if (_attackTimer > 0.0f) return;
        Vector3 diffrence = GameController.Me.Players[0].transform.localPosition - transform.localPosition;
        RaycastHit2D[] hits;
        if (Mathf.Abs(diffrence.x) > Mathf.Abs(diffrence.y))
        {
            hits = Physics2D.CircleCastAll(gameObject.transform.localPosition, 0.3f, Mathf.Sign(diffrence.x) * Vector3.right);
        }
        else
        {
            hits = Physics2D.CircleCastAll(gameObject.transform.localPosition, 0.3f, Mathf.Sign(diffrence.y) * Vector3.up);
        }
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                hit.transform.gameObject.GetComponent<PlayerController>().Hit();
        }
        _attackTimer = _attackDelay;
        Wander();
    }

    private void GetMovementDirection()
    {
        Vector3 diffrence = _currentElement.transform.localPosition - transform.localPosition;
        if (Mathf.Abs(diffrence.x) > Mathf.Abs(diffrence.y))
        {
            _movementDirection = Mathf.Sign(diffrence.x) * Vector3.right;
        }
        else
        {
            _movementDirection = Mathf.Sign(diffrence.y) * Vector3.up;
        }
        AttackTrigger.offset = _movementDirection * 0.4f;
    }

    public void Hit()
    {
        Debug.Log("OH NO!!! IM DYING!!!!!");
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MyState = EEnemyState.Fight;
            _target = col.gameObject;
            Wander();
        }
    }
}

public enum EEnemyState
{
    Wander,
    Fight
}
