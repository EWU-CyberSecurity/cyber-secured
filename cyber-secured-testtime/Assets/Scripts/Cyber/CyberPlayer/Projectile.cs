using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int weaponEffectID;
    [SerializeField] private float speed;

    private float direction;
    private bool hit;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        if(collision.transform.name == "Enemy1Temp(Clone)" || collision.transform.name == "Enemy2Temp(Clone)")
        {
            collision.gameObject.GetComponentInChildren<EnemyBase>().TakeDamage(damage);
            collision.gameObject.GetComponentInChildren<EnemyBase>().AddWeaponEffect(weaponEffectID);
        }
        Destroy(gameObject);
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != dir)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Dectivate()
    {
        gameObject.SetActive(false);
    }

    public int Damage
    {
        get{return damage;}
        set{damage = value;}
    }

    public int WeaponEffectID
    {
        get{return weaponEffectID;}
        set{weaponEffectID = value;}
    }
}
