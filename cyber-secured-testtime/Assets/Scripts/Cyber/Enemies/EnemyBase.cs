using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int speed;
    private int health;
    private bool isDead;
    private WeaponBase enemyWeapon;

    public int Speed
    {
        get{return speed;}
        set{speed = value;}
    }
    public int Health
    {
        get{return health;}
        set{health = value;}
    }

    public WeaponBase EnemyWeapon
    {
        get{return enemyWeapon;}
        set{enemyWeapon = value;}
    }

    public bool IsDead
    {
        get{return isDead;}
        set{isDead = value;}
    }

    public void Move()
    {
        //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + hMove, this.gameObject.transform.position.y + vMove, this.gameObject.transform.position.z);
    }

    public void Attack()
    {

    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
            IsDead = true;
    }

    public void WeaponEffect(int id)
    {
        
        switch(id)
        {
            case(0):
            {
                break;
            }
            case(1):
            {
                float burnTimer = 3.0f;
                while(burnTimer >= 0)
                {
                    burnTimer -= Time.deltaTime;
                    this.TakeDamage(1);
                }
                break;
            }
        }
    }
}