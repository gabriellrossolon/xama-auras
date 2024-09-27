using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject instantiator;
    void Start()
    {
        Destroy(this.gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<EntityStats>().DoDamage(instantiator.GetComponent<EntityStats>().attackDamage);
            Destroy(this.gameObject);
        }
    }
}
