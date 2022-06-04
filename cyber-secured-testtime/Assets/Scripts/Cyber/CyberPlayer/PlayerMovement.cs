using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] public LayerMask ground;

    public Sprite walkAnimation1;
    public Sprite walkAnimation2;
    public Sprite jumpAnimation1;
    public Sprite jumpAnimation2;
    public Sprite attackAnimation1;
    public Sprite attackAnimation2;
    public Sprite damageSprite1;
    public Sprite damageSprite2;

    private Rigidbody2D body;
    //public Collider2D footCollider;
    private BoxCollider2D boxCollider;

    private float horizontalInput;
    private bool canTakeDamage = true;
    private bool walking;
    private bool jumping;
    private float iFrame;
    private int walkCounter;
    private int attackCounter;

    public bool hit;
    public bool attacking;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(.45f, .45f, .45f);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            walking = true;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-.45f, .45f, .45f);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            walking = true;
        }
        else
            walking = false;

        
        if(hit)
        {
            canTakeDamage = false;
            if(iFrame <= .125f)
                this.gameObject.GetComponent<SpriteRenderer>().sprite = damageSprite1;
            else   
                this.gameObject.GetComponent<SpriteRenderer>().sprite = damageSprite2;

            iFrame += Time.deltaTime;

            if(iFrame > .25f)
            {
                canTakeDamage = true;
                hit = false;
                iFrame = 0.0f;
            }
            jumping = false;
            attacking = false;
        }
        else if(attacking)
        {
            attackCounter++;
            if(attackCounter < 7)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = attackAnimation1;
            }
            if (attackCounter == 7)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = attackAnimation2;
            }
            if(attackCounter >= 15)
            {
                attackCounter = 0;
                attacking = false;
            }
            jumping = false;
        }
        else if(walking && !jumping)
        {
            walkCounter++;
            if(walkCounter ==  20)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = walkAnimation1;
            }
            if (walkCounter == 40)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = walkAnimation2;
                walkCounter = 0;
            }
            jumping = false;
        }
        else if(!isGrounded() && jumping)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = jumpAnimation2;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = walkAnimation2;
            jumping = false;
        }
        

        if (Input.GetKey(KeyCode.W))
        {
            if (isGrounded())
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = jumpAnimation1;
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                jumping = true;
            }
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, ground);
        return raycastHit.collider != null;
    }

    public bool TakeDamage(int damage)
    {
        if(canTakeDamage)
        {
            this.gameObject.GetComponent<NPCharacterInterface>().NPDamage(damage);
            return true;
        }
        return false;
    }
}