using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    private GameObject playerObject;
    private EntityStats entityStats;
    private Animator anim;

    public GameObject projectilePrefab;

    private bool canAttack;
    private float attackCooldown;

    public int detectionRange;
    public int projectileSpeed;

    private void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        entityStats = GetComponent<EntityStats>();
        anim = GetComponent<Animator>();

        canAttack = true;
    }

    private void Update()
    {
        BasicAttack();
        BasicAttackCooldown();
    }

    private void BasicAttack()
    {
        float distance_to_player = Vector3.Distance(transform.position, playerObject.transform.position);
        if (canAttack && distance_to_player <= detectionRange)
        {
            StartCoroutine(ExecuteBasicAttack());
            canAttack = false;
            attackCooldown = 0;
        }
    }

    private IEnumerator ExecuteBasicAttack()
    {
        anim.SetTrigger("isAttack");

        // Espera 0,5 segundos antes de executar o restante do ataque
        yield return new WaitForSeconds(0.6f);

        // Aqui vai o restante do código do ataque
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileInstance.gameObject.GetComponent<ProjectileBehavior>().instantiator = this.gameObject;

        Vector3 playerPosition = playerObject.transform.position;
        Vector2 projectileDirection = (playerPosition - transform.position).normalized;

        float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
        projectileInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        projectileInstance.GetComponent<Rigidbody2D>().AddForce(projectileDirection * projectileSpeed, ForceMode2D.Impulse);  
    }

    //=====//=====//=====//=====//=====//=====//=====//=====//=====//=====//=====//=====//=====//=====//=====//=====//=====//

    private void BasicAttackCooldown()
    {
        if (attackCooldown >= entityStats.attackSpeed && canAttack == false)
        {
            canAttack = true;
            attackCooldown = 0;
        }
        else
        {
            attackCooldown += Time.deltaTime;
        }
    }
}

