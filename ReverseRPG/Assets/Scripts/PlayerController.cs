using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float SpeedMultiplier;
    //public CircleCollider2D AttackTrigger;
    public EGamePad PlayerPad;

    private float _attackDelay = 1.5f;
    private float _attackTimer = 0.0f;
    private Vector2 _attackDirection;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position += InputController.Me.GetLeftStick(PlayerPad) * SpeedMultiplier * Time.deltaTime;
	    _attackTimer += Time.deltaTime;
        
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
	    else
	    {
	        //AttackTrigger.offset = Vector2.zero;
	    }
	}

    public void Hit()
    {
        Debug.Log("No!!! Dont kill player");
    }

    private void Attack()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(gameObject.transform.localPosition, 0.3f, _attackDirection);
        foreach (RaycastHit2D hit in hits)
        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                hit.transform.gameObject.GetComponent<Enemy>().Hit();
        }
    }
}
