using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public CharacterClass MyClass = new CharacterClass();
    public float SpeedMultiplier;
    public EGamePad PlayerPad;

    public float AttackDelay = 1.5f;
    private float _attackTimer = 0.0f;
    private Vector2 _attackDirection;

    public void Start()
    {
        MyClass.StageStartLevel = MyClass.Level;
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
	    transform.position += InputController.Me.GetLeftStick(PlayerPad) * SpeedMultiplier * Time.deltaTime;

	    Vector3 attack = InputController.Me.GetRightStick(PlayerPad);
	    if (attack.magnitude > 0.3f)
	    {
	        if (Mathf.Abs(attack.x) > Mathf.Abs(attack.y))
	        {
                _attackDirection = new Vector2(Mathf.Sign(attack.x) * 0.5f, 0f);
                //AttackTrigger.offset = new Vector2(Mathf.Sign(attack.x)*1f,0f);
            }
	        else
	        {
                _attackDirection = new Vector2(0f, Mathf.Sign(attack.y) * 0.5f);
                //AttackTrigger.offset = new Vector2(0f, Mathf.Sign(attack.y) * 1f);
            }
            Attack();
	    }
	    _attackTimer -= Time.deltaTime;
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
        _attackTimer = AttackDelay;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(gameObject.transform.localPosition, 0.3f, _attackDirection);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.tag == "Enemy" && !hit.collider.isTrigger)
            {
                hit.transform.gameObject.GetComponent<Enemy>().Hit(this);
            }
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
