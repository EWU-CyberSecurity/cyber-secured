using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBase
{
    public Sprite animation1;
    public Sprite animation2;
    public Sprite deathSprite1;
    public Sprite deathSprite2;
    private Transform player;
    public float sight;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 3;
        Health = 2;
        EnemyWeapon = new WeaponBase();
        EnemyWeapon.WeaponName = "E2";
        EnemyWeapon.WeaponID = 2;
        EnemyWeapon.Damage = 1;
        EnemyWeapon.AttackSpeed = 0.5f;
        EnemyWeapon.WeaponEffectID = 0;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        sight = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < sight)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
        }

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

            if(timeSinceAttack >= 0)
            {
                timeSinceAttack += Time.deltaTime;
            }
            
            if(effectTickRate <= timeSinceEffect)
                CheckStatus();
            else
                timeSinceEffect += Time.deltaTime;

            Move(-1, 1, Speed);
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,sight);
    }

    void Attack()
    {

    }
}