using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase
{
    private int weaponID;
    private string weaponName;
    private int damage;
    private float attackSpeed;
    private int weaponEffectID;
    
    public int WeaponID
    {
        get{return weaponID;}
        set{weaponID = value;}
    }
    public string WeaponName
    {
        get{return weaponName;}
        set{weaponName = value;}
    }

    public int Damage
    {
        get{return damage;}
        set{damage = value;}
    }

    public float AttackSpeed
    {
        get{return attackSpeed;}
        set{attackSpeed = value;}
    }

    public int WeaponEffectID
    {
        get{return weaponEffectID;}
        set{weaponEffectID = value;}
    }
}