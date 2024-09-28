using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMAttack : MonoBehaviour
{
    public GameObject playerWeapon;
    public Transform weaponParent;
    private EntityStats entityStats;
    private SkillController skillController;

    private float attackCooldown;
    private bool canAttack = true;

    private void Start()
    {
        entityStats = GetComponent<EntityStats>();
        skillController = GetComponent<SkillController>();
    }
    void Update()
    {
        if (skillController.canUseAttack) { PlayerAttack(); }
        WeaponRotation();
        AttackCooldown();
        
    }

    private void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(ActiveWeaponCollider());
            canAttack = false;
            attackCooldown = 0;
        }
    }

    private IEnumerator ActiveWeaponCollider()
    {
        playerWeapon.GetComponent<Animator>().SetTrigger("isAttack");
        playerWeapon.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        playerWeapon.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void WeaponRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // Distância da câmera
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = worldMousePos - weaponParent.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponParent.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void AttackCooldown()
    {
        if(attackCooldown >= entityStats.attackSpeed && !canAttack)
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
