using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int speed;
    private int health;
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

    public void move()
    {
        //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + hMove, this.gameObject.transform.position.y + vMove, this.gameObject.transform.position.z);
    }

    public void attack()
    {

    }
}