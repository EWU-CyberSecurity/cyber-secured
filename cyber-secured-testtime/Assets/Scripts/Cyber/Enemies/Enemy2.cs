using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBase
{
    public Sprite animation1;
    public Sprite animation2;
    public Sprite deathSprite1;
    public Sprite deathSprite2;

    private int counter;
    private float deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 2;
        Health = 2;
        WeaponBase tempWeapon = new WeaponBase();
        tempWeapon.WeaponName = "E2";
        tempWeapon.WeaponID = 2;
        tempWeapon.Damage = 2;
        tempWeapon.AttackSpeed = 0.5f;
        tempWeapon.WeaponEffectID = 0;
        EnemyWeapon = tempWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if(IsDead != true)
        {
            if(counter ==  20)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = animation1;
            }
            if (counter == 40)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = animation2;
                counter = 0;
            }
        }
        else
        {
            if(deathTimer <= .125f)
                this.gameObject.GetComponent<SpriteRenderer>().sprite = deathSprite1;
            else   
                this.gameObject.GetComponent<SpriteRenderer>().sprite = deathSprite2;

            deathTimer += Time.deltaTime;

            if(deathTimer > .25f)
            {
                deathTimer = 0.0f;
                Destroy(gameObject);
            }
        }

        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - 0.01f, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<CyberCharacter>().TakeDamage(EnemyWeapon.Damage);
        }
    }
}