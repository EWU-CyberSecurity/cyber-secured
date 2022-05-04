using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase
{
    private int speed;
    private int health;
    private WeaponBase enemyWeapon;
    private enum enemyTypes
    {
        ENEMY1,
        ENEMY2,
        ENEMY3
    }

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
        //transform.Translate(Vector3(-1.0, 0.0, 0.0) * Time.deltaTime);
    }

    public void attack()
    {

    }
}
