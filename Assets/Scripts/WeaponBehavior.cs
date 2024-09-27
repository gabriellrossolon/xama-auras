using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    private GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EntityStats>().DoDamage(playerObject.GetComponent<EntityStats>().attackDamage);
        }
        else if (collision.CompareTag("Destroyable"))
        {
            if(playerObject.GetComponent<TransformBehavior>().actualTransformation == "Urso")
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Não tem força para destruir esse Obstaculo");
            }
            
        }
    }
}
