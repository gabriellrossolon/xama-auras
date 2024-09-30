using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    private GameObject playerObject;
    public AudioSource attackHitAudioSource;
    public AudioClip attackEnemyHitAudioClip;
    public AudioClip attackObjectHitAudioClip;

    private void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EntityStats>().DoDamage(playerObject.GetComponent<EntityStats>().attackDamage);
            attackHitAudioSource.PlayOneShot(attackEnemyHitAudioClip);
        }
        else if (collision.CompareTag("Destroyable"))
        {
            if(playerObject.GetComponent<TransformBehavior>().actualTransformation == "Urso")
            {
                collision.GetComponent<Animator>().SetTrigger("isDestruct");
                Destroy(collision.gameObject, 0.3f);

            }
            else
            {
                Debug.Log("Não tem força para destruir esse Obstaculo");
            }
            attackHitAudioSource.PlayOneShot(attackObjectHitAudioClip);
        }
    }
}
