using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int speed;
    private int health;
    private bool isDead;
    protected int counter;
    protected float deathTimer;
    protected float timeSinceAttack;
    protected float timeSinceEffect;
    protected float effectTickRate;
    private int[] activeStatusArray = new int[2];
    private int[] statusDurationArray = new int[2];
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

    public void Move(int x, int y, int speed)
    {
        float hSpeed = x * (Speed * 0.01f);
        float ySpeed = y * (Speed * 0.01f);

        if(hSpeed < 0)
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if(hSpeed > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + hSpeed, this.gameObject.transform.position.y + ySpeed, this.gameObject.transform.position.z);
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log("Remaining health: " + Health);
        if(Health <= 0)
            IsDead = true;
    }

    public void AddWeaponEffect(int id)
    {
        switch(id)
        {
            case(0):
            {
                break;
            }
            case(1):
            {
                if(activeStatusArray[1] == 1)
                    return;
                else
                {
                    activeStatusArray[1] = 1;
                    statusDurationArray[1] = 2;
                }
                break;
            }
        }
    }

    public void CheckStatus()
    {
        if(activeStatusArray[0] == 1)
        {

        }

        if(activeStatusArray[1] == 1)
        {
            statusDurationArray[1] = statusDurationArray[1] - 1;
            Debug.Log("Enemy is burned, remaining duration is " + statusDurationArray[1]);
            TakeDamage(1);
            timeSinceEffect = 0.0f;
            if(statusDurationArray[1] <= 0)
                activeStatusArray[1] = 0;
        }
    }
}