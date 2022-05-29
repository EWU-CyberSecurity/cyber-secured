using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBase
{
    public Sprite animation1;
    public Sprite animation2;
    public Sprite animation3;
    public Sprite deathSprite;
    public Transform player;
    public float sight;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 3;
        Health = 4;
        EnemyWeapon = new WeaponBase();
        EnemyWeapon.WeaponName = "E1";
        EnemyWeapon.WeaponID = 1;
        EnemyWeapon.Damage = 2;
        EnemyWeapon.AttackSpeed = 1.0f;
        EnemyWeapon.WeaponEffectID = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sight = 10.0f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (transform.position.x > 0.01f)
        {
            transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        else if (transform.position.x < -0.01f)
        {
            transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);
        }*/

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
        

        if (IsDead != true)
        {
            counter++;
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

            timeSinceAttack += Time.deltaTime;

            CheckStatus();

            Move(-1, 0, Speed);
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sight);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player" && EnemyWeapon.AttackSpeed <= timeSinceAttack)
        {
            collision.gameObject.GetComponent<CyberCharacter>().TakeDamage(EnemyWeapon.Damage);
            timeSinceAttack = 0.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "ground":
                rb.AddForce(Vector2.up * 150f);
                break;
        }
    }
}