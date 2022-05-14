using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - 0.01f, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z);
    }

    public Enemy2()
    {
        Speed = 2;
        Health = 2;
        WeaponBase tempWeapon = new WeaponBase();
        tempWeapon.WeaponName = "E1";
        tempWeapon.WeaponID = 1;
        tempWeapon.Damage = 2;
        tempWeapon.AttackSpeed = 0.5f;
        tempWeapon.WeaponEffectID = 0;
        EnemyWeapon = tempWeapon;
    }
}