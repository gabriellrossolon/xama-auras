using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleAttack : MonoBehaviour
{
    private EntityStats entityStats;
    private Animator anim;

    private bool canAttack = true;
    private float attackCooldown;

    void Start()
    {
        entityStats = GetComponent<EntityStats>();
        anim = GetComponent<Animator>();
        canAttack = true;
    }

    void Update()
    {
        AttackCooldown();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canAttack)
        {
            anim.SetTrigger("isAttack");
            collision.GetComponent<EntityStats>().DoDamage(entityStats.attackDamage);
            canAttack = false;
            attackCooldown = 0;

            StartCoroutine(ManageCollider());
        }
    }

    private void AttackCooldown()
    {
        if(attackCooldown >= entityStats.attackSpeed)
        {
            canAttack = true;
            attackCooldown = 0;
        }
        else
        {
            attackCooldown += Time.deltaTime;
        }
    }

    private IEnumerator ManageCollider()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(entityStats.attackSpeed);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
