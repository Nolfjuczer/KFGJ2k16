using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public CharacterClass MyClass = new CharacterClass();
    public float SpeedMultiplier;
    public EGamePad PlayerPad;

    public float AttackDelay = 0.3f;
    private float _attackTimer = 0.0f;
    private Vector2 _attackDirection;
    private Animator _myAnimator;
    private bool _spell;
    public void Start()
    {
        MyClass.StageStartLevel = MyClass.Level;
        _myAnimator = gameObject.GetComponent<Animator>();
    }

    public int GetAttackPower()
    {
        int dmg = 0;
        dmg += MyClass.Strenght;
        foreach (Weapon weapon in MyClass.Weapons)
        {
            dmg += weapon.Attack;
        }
        return dmg;
    }

    public void DecreaseExp(int exp)
    {
        UIController.Me.UseExp(transform.position,(-exp).ToString());
        MyClass.EXP -= exp;
    }
	
	void Update ()
	{
	    if (GameController.Me.GameState != EGameState.GAME) return;
        _attackTimer -= Time.deltaTime;
        Vector3 move = InputController.Me.GetLeftStick(PlayerPad);
	    if (move.magnitude > 0.1f)
	    {
            if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
                _attackDirection = new Vector2(Mathf.Sign(move.x), 0f);
            else
                _attackDirection = new Vector2(0f, Mathf.Sign(move.y));
            transform.position += (Vector3)_attackDirection * SpeedMultiplier * Time.deltaTime;
            ProcessWalkAnimation();
            //if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
            //    ProcessWalkAnimation();
            //else
            //    ProcessWalkAnimation();	        
	    }
	    else
	    {
	        _myAnimator.SetBool("WALK",false);
	    }
        
        if(InputController.Me.GetLeftBumper(PlayerPad)) SpellLeft();
        if(InputController.Me.GetRightBumper(PlayerPad)) SpellRight();
        if(InputController.Me.GetX(PlayerPad)) Attack();
        //Vector3 attack = InputController.Me.GetRightStick(PlayerPad);
        //if (attack.magnitude > 0.3f)
        //{
        //    if (Mathf.Abs(attack.x) > Mathf.Abs(attack.y))
        //           _attackDirection = new Vector2(Mathf.Sign(attack.x), 0f);
        //    else
        //           _attackDirection = new Vector2(0f, Mathf.Sign(attack.y));
        //       Attack();
        //}

	}

    public void SpellLeft()
    {
        Spell();
    }

    public void Spell()
    {
        if (_attackTimer > 0.0f) return;
        //Vector3 dir = InputController.Me.GetLeftStick(PlayerPad);
        //if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        //    ProcessWalkAnimation(new Vector2(Mathf.Sign(dir.x), 0f));
        //else
        //    ProcessWalkAnimation(new Vector2(0f, Mathf.Sign(dir.y)));
        _attackTimer = AttackDelay;
        ProcessSpellAnimation();
    }

    public void SpellRight()
    {
        Spell();
    }

    public void Hit(Enemy enemy)
    {
        int dmg = enemy.GetAttackPower();
        foreach (Armor armor in MyClass.Armors)
        {
            dmg -= armor.Defense;
        }
        MyClass.HP -= dmg;
        UIController.Me.UseDamage(gameObject.transform.position, dmg.ToString());
        if (MyClass.HP <= 0)
        {
            //todo kill player
            gameObject.SetActive(false);
            GameController.Me.CheckPlayersAlive();
        }
    }

    private void Attack()
    {
        if (_attackTimer > 0.0f) return;
        ProcessAtackAnimation();
        _attackTimer = AttackDelay;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(gameObject.transform.localPosition, 0.3f, _attackDirection,0.7f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.tag == "Enemy" && !hit.collider.isTrigger)
            {
                hit.transform.gameObject.GetComponent<Enemy>().Hit(this);
            }
        }
    }

    public void ResetAllActions()
    {
        _myAnimator.SetBool("ATTACK", false);
        _myAnimator.SetBool("SPELL", false);
        _myAnimator.SetBool("WALK", false);
        _myAnimator.SetBool("DEAD", false);
    }

    public void ProcessSpellAnimation()
    {
        ProcessActionDirection(_attackDirection);
        _myAnimator.SetBool("SPELL", true);
    }

    public void ProcessAtackAnimation()
    {
        ProcessActionDirection(_attackDirection);
        _myAnimator.SetBool("ATTACK",true);
    }

    public void ProcessWalkAnimation()
    {
        if (_myAnimator.GetBool("ATTACK") || _myAnimator.GetBool("SPELL")) return;
        ProcessActionDirection(_attackDirection);
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

    public void OnStageOver()
    {
        if (MyClass.EXP > (int)ELevels.Level1)
        {
            MyClass.Level = ELevels.Level1;
        }
        if (MyClass.EXP > (int)ELevels.Level2)
        {
            MyClass.Level = ELevels.Level2;
        }
        if (MyClass.EXP > (int)ELevels.Level3)
        {
            MyClass.Level = ELevels.Level3;
        }
        if (MyClass.EXP > (int)ELevels.Level4)
        {
            MyClass.Level = ELevels.Level4;
        }
        if (MyClass.EXP > (int) ELevels.Level5)
        {
            MyClass.Level = ELevels.Level5;
        }
        if (MyClass.EXP > (int) ELevels.Level6)
        {
            MyClass.Level = ELevels.Level6;
        }

        if (MyClass.Level != MyClass.StageStartLevel)
        {
            AffectLevelDecrease();
        }
    }

    public void AffectLevelDecrease()
    {
        //todo removing spells stats etc
    }
}

public enum EDirection
{
    Left,
    Right,
    Front,
    Back
}
