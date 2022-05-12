using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform ProjPoint;
    [SerializeField] private GameObject[] Proj;

    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    // Start is called before the first frame update
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && cooldownTimer > attackCooldown /*&& playerMovement.canAttack()*/)
            Attack();

        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        cooldownTimer = 0;

        Proj[FindProjectile()].transform.position = ProjPoint.position;
        Proj[FindProjectile()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindProjectile()
    {
        for (int i = 0; i < Proj.Length; i++)
        {
            if (!Proj[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
