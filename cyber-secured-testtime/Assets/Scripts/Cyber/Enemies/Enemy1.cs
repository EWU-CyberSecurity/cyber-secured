using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBase
{
    public Sprite animation1;
    public Sprite animation2;
    public Sprite animation3;
    public Sprite deathSprite;
    private int counter;
    private float deathTimer;


    // Start is called before the first frame update
    void Start()
    {
        Speed = 4;
        Health = 4;
        WeaponBase tempWeapon = new WeaponBase();
        tempWeapon.WeaponName = "E1";
        tempWeapon.WeaponID = 1;
        tempWeapon.Damage = 4;
        tempWeapon.AttackSpeed = 1.0f;
        tempWeapon.WeaponEffectID = 0;
        EnemyWeapon = tempWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if(IsDead != true)
        {
            if(counter ==  7)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = animation1;
            }
            if (counter == 14)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = animation2;
            }
            if (counter == 28)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = animation3;
                counter = 0;
            }
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = deathSprite;

            deathTimer += Time.deltaTime;
            if(deathTimer > .25f)
            {
                deathTimer = 0.0f;
                Destroy(gameObject);
            }
        }
        
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - 0.01f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "CyberCharacter")
        {
            collision.gameObject.GetComponent<CyberCharacter>().TakeDamage(EnemyWeapon.Damage);
        }
    }
}