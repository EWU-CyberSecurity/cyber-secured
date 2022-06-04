using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform ProjPoint;
    [SerializeField] private Transform projectile;
    [SerializeField] private WeaponBase currentWeapon;
    [SerializeField] private WeaponBase defaultWeapon;

    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    // Start is called before the first frame update
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        
        defaultWeapon = new WeaponBase();
        defaultWeapon.WeaponName = "Default"; 
        defaultWeapon.WeaponID = 0;
        defaultWeapon.Damage = 1;
        defaultWeapon.WeaponEffectID = 0;
        defaultWeapon.AttackSpeed = .75f;
        
        currentWeapon = defaultWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && cooldownTimer > currentWeapon.AttackSpeed)
        {
            playerMovement.attacking = true;
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }
    
    private void Attack()
    {
        cooldownTimer = 0;

        Transform proj = Instantiate(projectile, ProjPoint.position, ProjPoint.rotation);
        proj.GetComponent<Projectile>().Damage = currentWeapon.Damage;
        proj.GetComponent<Projectile>().WeaponEffectID = currentWeapon.WeaponEffectID;
        proj.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    public void NewWeapon(WeaponBase newWeapon)
    {
        currentWeapon = newWeapon;
    }
}
