using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

public class Enemy : MonoBehaviour
{
    public CharacterStats MyStats = new CharacterStats();
    public float SpeedMultiplier;
    public CircleCollider2D AttackTrigger;
    public EEnemyState MyState;

    private AStar.AStarAgent _myAgent;
    private AStar.GridElement _currentElement;
    private Vector3 _movementDirection;
    private GameObject _target;
    private float _attackTimer;
    public float AttackDelay;

    private float _distanceToNext;
    private Animator _myAnimator;
	// Use this for initialization
	void Start ()
	{
        _myAgent = GetComponent<AStar.AStarAgent>();
        MyState = EEnemyState.Wander;
	    _myAnimator = GetComponent<Animator>();
        Wander();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameController.Me.GameState != EGameState.GAME) return;
        GoToPosition();
	    _attackTimer -= Time.deltaTime;
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
            _distanceToNext = float.MaxValue;
            _myAgent.TargetObject = _target.transform;
        }
        _myAgent.CalculatePath();
        GetNextElement();
    }

    public void ResetAllActions()
    {
        _myAnimator.SetBool("ATTACK", false);
        _myAnimator.SetBool("WALK", false);
    }


    public void ProcessAtackAnimation(Vector2 vec)
    {
        ProcessActionDirection(vec);
        _myAnimator.SetBool("ATTACK", true);
    }

    public void ProcessWalkAnimation()
    {
        if (_myAnimator.GetBool("ATTACK")) return;
        ProcessActionDirection(_movementDirection);
        _myAnimator.SetBool("WALK", true);
    }

    public void ProcessActionDirection(Vector2 vector)
    {
        if (vector == Vector2.left)
        {
            _myAnimator.SetBool("LEFT", true);
            _myAnimator.SetBool("RIGHT", false);
            _myAnimator.SetBool("BACK", false);
            _myAnimator.SetBool("FRONT", false);
        }
        else if (vector == Vector2.down)
        {
            _myAnimator.SetBool("LEFT", false);
            _myAnimator.SetBool("RIGHT", false);
            _myAnimator.SetBool("BACK", false);
            _myAnimator.SetBool("FRONT", true);
        }
        else if (vector == Vector2.right)
        {
            _myAnimator.SetBool("LEFT", false);
            _myAnimator.SetBool("RIGHT", true);
            _myAnimator.SetBool("BACK", false);
            _myAnimator.SetBool("FRONT", false);
        }
        else
        {
            _myAnimator.SetBool("LEFT", false);
            _myAnimator.SetBool("RIGHT", false);
            _myAnimator.SetBool("BACK", true);
            _myAnimator.SetBool("FRONT", false);
        }
    }

    private void GoToPosition()
    { 
        ProcessWalkAnimation();
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
                {
                    if (_attackTimer <= 0)
                        Attack();
                    else
                        return;
                }
                else
                {
                    if (_currentElement != null)
                    {
                        _distanceToNext = Vector3.Distance(transform.localPosition, _currentElement.transform.localPosition);
                        transform.localPosition += _movementDirection * Time.deltaTime * SpeedMultiplier;
                        float currentDistance = Vector3.Distance(transform.localPosition, _currentElement.transform.localPosition);
                        if (currentDistance > _distanceToNext)
                            Wander();
                        if (_distanceToNext < 0.1f)
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
            hits = Physics2D.CircleCastAll(gameObject.transform.localPosition, 0.3f, Mathf.Sign(diffrence.x) * Vector3.right,0.7f);
            ProcessAtackAnimation(new Vector2(Mathf.Sign(diffrence.x),0.0f));
        }
        else
        {
            hits = Physics2D.CircleCastAll(gameObject.transform.localPosition, 0.3f, Mathf.Sign(diffrence.y) * Vector3.up,0.7f);
            ProcessAtackAnimation(new Vector2(0.0f, Mathf.Sign(diffrence.y)));
        }
        
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.tag == "Player")
                hit.transform.gameObject.GetComponent<PlayerController>().Hit(this);
        }
        _attackTimer = AttackDelay;
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

    public void Hit(PlayerController player)
    {
        MyStats.HP -= player.GetAttackPower();
        UIController.Me.UseDamage(gameObject.transform.position, player.GetAttackPower().ToString());
        if (MyStats.HP <= 0)
        {
            player.DecreaseExp(MyStats.EXP);
            //todo pooling
            gameObject.SetActive(false);
        }
    }

    public int GetAttackPower()
    {
        return MyStats.Strenght;
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
