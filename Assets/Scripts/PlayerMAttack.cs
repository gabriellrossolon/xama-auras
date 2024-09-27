using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMAttack : MonoBehaviour
{
    public GameObject playerWeapon;
    void Start()
    {
        
    }
    void Update()
    {
        PlayerAttack();
    }

    private void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ActiveWeaponCollider());
        }
    }

    private IEnumerator ActiveWeaponCollider()
    {
        playerWeapon.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        playerWeapon.GetComponent<BoxCollider2D>().enabled = false;
    }
}
